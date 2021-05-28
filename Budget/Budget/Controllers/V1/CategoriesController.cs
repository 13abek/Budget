using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Budget.Data;
using Budget.Data.Entities;
using AutoMapper;
using Budget.Filters;
using Budget.Libs;
using Budget.Resources.Category;


namespace Budget.Controllers.V1
{
    [Route("api/v1/categories")]
    [ApiController]
    [TypeFilter(typeof(UserAuthFilter))] 
    public class CategoriesController : ControllerBase
    {
        private User _user => RouteData.Values["User"] as User;
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        //private readonly IUserAuth _userAuth; //2 way Authorization Servic logic 
        public CategoriesController(BudgetDbContext context,IMapper mapper /*,IUserAuth userAuth*/,IFileManager fileManager)
        {
            _context = context;
            _mapper = mapper;
            //_userAuth = userAuth; 
            _fileManager = fileManager;
        }
        [HttpGet]
        [Route("")]
        public IActionResult CategoriesList()
        {
            /*if (_userAuth.user == null) return Unauthorized();*/ //Autorization way service logic

            var categories = _context.Categories.Where(c => c.Status && c.UserId == _user.id).OrderBy(c => c.OrderBy).ToList();
            var caregoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return Ok(caregoryResource);
        }

        [HttpGet]
        [Route("[action]")]
        public  IActionResult SearchCategory(string type)
        {
            var category = _context.Categories.Where(c => c.Name.StartsWith(type));
                return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult CategoryById(int id )
        {
            var category = _context.Categories.Find(id); //bu id-li  category var ?
                if(category == null) return NotFound(); // bu id category yoxdursa "notfound"

            if (category.UserId != _user.id) return NotFound(); //  category-deki userin id i ile userid bereber deyilse yene "notfound" user bashqa userin adi ile sorgu gomderir demkdir!

            var categoryResource = _mapper.Map<Category, CategoryResource>(category);


       
            return Ok(categoryResource);
        }
      

        [HttpPost]
        [Route("")]
        public  IActionResult CreateCategory([FromBody]CreateCategoryResource resource)
        {
            if (!ModelState.IsValid) return BadRequest(); //Validationlara ters bir wey olanda verilecek error

            if (_context.Categories.Any(c => c.Status && c.Name == resource.Name && c.UserId == _user.id)) return Conflict();  // yaradilacq category-inin db olub ve ya olmadigini yoxlayir,eger vaarsa confiliq qaytaracaq

            if (!_fileManager.FileExists(resource.Icon)) return NotFound(); // gonderilen icon _filemanager icinde yoxdursa bu error qayitmalidir (ilk once _filemanagere upload olmalidi gonderilecek sekil! 1(Libs/FileManager || FileManagerController)-> sekili ilk once bunlarin komeyi ile upload edirik

            var category = _mapper.Map<CreateCategoryResource, Category>(resource);

            category.UserId = _user.id;
            category.AddedBy = _user.Fullname;
            category.OrderBy = _context.Categories.Where(c => c.Status && c.UserId == _user.id).Count() + 1; // orderBy sonuncuya qoymaq ucun ,ilk once sonuncuyu tapib ustune bir gelirik

            _context.Categories.Add(category);
            _context.SaveChanges();


            var categoryResource = _mapper.Map<Category, CategoryResource>(category); //categoryResource yaradib save olunan datanin geri qaytarib  gosteririk 
            
            return Ok(categoryResource);
        }  


        [HttpPut]
        [Route("{id}")]
        public IActionResult EditCategory(int id,[FromBody]EditCategoryResource resource)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != resource.id) return NotFound(); // update olunacaq resource-nin id ile  id beraber deyilse notfound

            var category = _context.Categories.Find(id); //id gore update olunmali category  tapilir

            if (category==null || category.UserId != _user.id) return NotFound(); //update olunacaq category userid sili ile girish edenin _user.id eyni deyilse notfound varilmelidir

            if (_context.Categories.Any(c => c.Status && c.UserId == _user.id && c.Name == resource.Name && c.id != resource.id)) return Conflict();   //c.id!=resource.id (bu koda gore bashqalarini icinde axtar category id updateolunanin id beraber olmayanlarin adini axtar)  // user update edib category -nin adini deyishdirmek isteyir amma o ad var oldugu zaman verilecek error 

            if (!_fileManager.FileExists(resource.Icon)) return StatusCode(419);  // update olunacaq resourc icon yoxdusa verilecek error-CODE 

            category.Name = resource.Name;
            category.Icon = resource.Icon;
            category.ModifiedBy = _user.Fullname;
            category.ModifiedDate = DateTime.Now;

            _context.SaveChanges();


            var categoryResource = _mapper.Map<Category, CategoryResource>(category); //update oluan category resource yaradilib geri  qaytarilir

            return Ok(categoryResource);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null || category.UserId != _user.id) return NotFound();

            _fileManager.Delete(category.Icon);  //file silinir

            _context.Categories.Remove(category);
            _context.SaveChanges();  //db category silinir

            /*  return NoContent();*/ // or 

            var categoryResource = _mapper.Map<Category,CategoryResource>(category);
            return Ok(categoryResource); // silinen data geri qaytaririq
        }
    }
}