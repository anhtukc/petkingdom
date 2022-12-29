
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq.Dynamic.Core;

namespace PetKingdomFN.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetKingdomContext _DbContext;
        private readonly ICloudStorageService _cloud;
        public PetRepository(PetKingdomContext DbContext, ICloudStorageService cloud)
        {
            _DbContext = DbContext;
            _cloud = cloud;
        }
        public async Task<DataList<Pet>> GetPageList(Pagination page)
        {
            DataList<Pet> result = new DataList<Pet>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Pet> allData = await _DbContext.Pets.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }
        public Task<List<Pet>> GetPetByCustomerId(string customerId)
        {
            return _DbContext.Pets.Where(x=>x.CustomerId == customerId && x.Status == 1).ToListAsync();
        }
        public async Task<DataList<Pet>> SearchPet(Pagination page, basedSearchObject searchObj)
        {
            DataList<Pet> result = new DataList<Pet>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Pet> allData = await _DbContext.Pets
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name))
                &&(searchObj.status ==-1|| x.Status == searchObj.status))
                .OrderBy(sortQuery)
                .ToListAsync();
            if (allData.Count() > 0)
            {
                result.numberOfRecords = allData.Count();

                result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize)
                    .ToList();
            }

            return result;
        }
        public async Task<Pet> AddPet(Pet pet)
        {
           
            pet.Birthday = DateTime.Parse(pet.birthDayFormat);
            pet.Id = Guid.NewGuid().ToString();
            if (!(pet.file is null))
            {
                pet.Image = await _cloud.UploadFileAsync(pet.file,pet.Id);
            }
            pet.CreatedDate = DateTime.Now;
            pet.UpdateDate = DateTime.Now;
            var obj = _DbContext.Pets.AddAsync(pet);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Pet> UpdatePet(Pet pet)
        {
            pet.UpdateDate = DateTime.Now;
            _DbContext.Entry(pet).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return pet;
        }
        public async Task<Pet> GetPetById(string id)
        {
            var obj = await _DbContext.Pets.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeletePet(string id)
        {
            var Pet = await _DbContext.Pets.FindAsync(id);
            if (Pet is null)
            {
                return 0;
            }
            _DbContext.Pets.Remove(Pet);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
