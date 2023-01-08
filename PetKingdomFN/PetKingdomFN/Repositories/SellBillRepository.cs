using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetKingdomFN.Helpers;
using Google.Api.Gax;

namespace PetKingdomFN.Repositories
{
    public class SellBillRepository : ISellBillRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public SellBillRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<List<CountStatus>> GetAllStatus()
        {
            var counts = _DbContext.SellBills
                        .GroupBy(x => x.Status)
                       .Select(g => new CountStatus { status = g.Key, quantityOfStatus = g.Count() })
                       .ToList();
            return counts;
        }
        public async Task<DataList<SellBill>> GetPageList(Pagination page, basedSearchObject searchObj)
        {
            DataList<SellBill> result = new DataList<SellBill>();

            string sortQuery = page.sortColumn + " " + page.sortOrder;

            var sellBills = await (from sb in _DbContext.SellBills
                            join a in _DbContext.Accounts on sb.CustomerAccountId equals a.Id     
                            join k in _DbContext.Accounts on sb.EmployeeAccountId equals k.Id
                            join d in _DbContext.Customers on a.Id equals d.AccountId
                            join e in _DbContext.Employees on k.Id equals e.AccountId
                            where sb.Status == searchObj.status
                            &&(searchObj.startDate =="0001-01-01"||sb.CreatedDate >= DateTime.Parse(searchObj.startDate))
                            && (searchObj.endDate == "9999-01-01" || sb.CreatedDate <= DateTime.Parse(searchObj.endDate))
                            &&(searchObj.phoneNumber == null||e.Phonenumber.Contains(searchObj.phoneNumber))
                            select new SellBill
                            {
                                Id = sb.Id,
                                CreatedDate = sb.CreatedDate,
                                TotalPaid = sb.TotalPaid,
                                CustomerAccountId = sb.CustomerAccountId,
                                EmployeeAccountId = sb.EmployeeAccountId,
                                Status = sb.Status,
                                BillType = sb.BillType,
                                UpdateDate = sb.UpdateDate,
                                CustomerAccount = new Account
                                {
                             
                                    Id = a.Id,
                                   
                                },
                                customer = new Customer
                                {
                                    Id = d.Id,
                                    FirstName = d.FirstName,
                                    LastName = d.LastName,
                                    Phonenumber = d.Phonenumber,
                                    Email = d.Email
                                }

                            }).OrderBy(sortQuery).ToListAsync();


            result.numberOfRecords = sellBills.Count();
            result.list = sellBills
                .Skip((page.currentPage - 1) * page.pageSize)              
                .Take(page.pageSize)
                .ToList();

            return result;
        }

        public async Task<DataList<SellBill>> GetPageByStatus(Pagination page, basedSearchObject searchObj)
        {
            DataList<SellBill> result = new DataList<SellBill>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<SellBill> allData = await _DbContext.SellBills
                .Where(x =>  (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
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
        public async Task<SellBill> AddSellBill(SellBill sellBill)
        {
            sellBill.Id = Guid.NewGuid().ToString();    
            sellBill.CreatedDate = DateTime.Now;
            sellBill.UpdateDate = DateTime.Now;           
             _DbContext.SellBills.AddAsync(sellBill);
            await _DbContext.SaveChangesAsync();
            var obj = await (from sb in _DbContext.SellBills
                             join a in _DbContext.Accounts on sb.CustomerAccountId equals a.Id
                             join k in _DbContext.Accounts on sb.EmployeeAccountId equals k.Id
                             join d in _DbContext.Customers on a.Id equals d.AccountId
                             join e in _DbContext.Employees on k.Id equals e.AccountId
                             where sb.Id == sellBill.Id
                             select new SellBill
                             {
                                 Id = sb.Id,
                                 CreatedDate = sb.CreatedDate,
                                 TotalPaid = sb.TotalPaid,
                                 CustomerAccountId = sb.CustomerAccountId,
                                 EmployeeAccountId = sb.EmployeeAccountId,
                                 Status = sb.Status,
                                 BillType = sb.BillType,
                                 UpdateDate = sb.UpdateDate,
                                 CustomerAccount = new Account
                                 {

                                     Id = a.Id,

                                 },
                                 customer = new Customer
                                 {
                                     Id = d.Id,
                                     FirstName = d.FirstName,
                                     LastName = d.LastName,
                                     Phonenumber = d.Phonenumber,
                                     Email = d.Email
                                 }

                             }).FirstAsync();
            return obj;
        }
        public async Task<SellBill> UpdateSellBill(SellBill SellBill)
        {
            SellBill.UpdateDate = DateTime.Now;
            _DbContext.Entry(SellBill).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return SellBill;
        }
      
        public async Task<SellBill> GetSellBillById(string id)
        {
            var obj = await _DbContext.SellBills.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteSellBill(string id)
        {
            var SellBill = await _DbContext.SellBills.FindAsync(id);
            List<Schedule> childSchedule = await _DbContext.Schedules.Where(x=>x.SellBillId  == id).ToListAsync();
           if(childSchedule.Count > 0)
            {
                List<Shift> childShift = new List<Shift>();
                for (int i = 0; i < childSchedule.Count; i++)
                {
                    var shift = await _DbContext.Shifts.Where(x => x.ScheduleId == childSchedule[i].Id).FirstOrDefaultAsync();
                    if(!(shift is null))
                    {
                        childShift.Add(shift);
                    }
                }
               if(!(childShift is null)){
                    _DbContext.Shifts.RemoveRange(childShift);
                }
            }
             _DbContext.Schedules.RemoveRange(childSchedule);

            if (SellBill is null)
            {
                return 0;
            }
            _DbContext.SellBills.Remove(SellBill);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
