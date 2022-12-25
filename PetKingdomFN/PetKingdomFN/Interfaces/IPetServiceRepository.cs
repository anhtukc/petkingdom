﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;
namespace PetKingdomFN.Interfaces
{
    public interface IPetServiceRepository
    {
        Task<List<PetService>> GetAll();

        Task<DataList<PetService>> GetPageList(Pagination page);
        Task<PetService> GetPetServiceById(string id);
        Task<PetService> AddPetService(PetService service);
        Task<PetService> UpdatePetService(PetService service);
        Task<int> DeletePetService(string id);

        Task<DataList<PetService>> SearchPetService(Pagination page, basedSearchObject searchObj);

    }
}
