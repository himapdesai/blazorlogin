using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorLogin.Data;
using ErrorBlazorLoginApp.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using SQLitePCL;
using Microsoft.Net.Http.Headers;

namespace ErrorBlazorLoginApp.Services
{

    public class ErrorService
    {

        private readonly AppDbContext _db;

        public ErrorService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Error>> GetErrors()
        {
            return await _db.Errors.ToListAsync();
        }

        public async Task<Error?> GetErrorById(int Id)
        {
            var errors = await this.GetErrors();
            return errors.FirstOrDefault(e => e.ID == Id);
        }
        public async Task SaveError(Error error)
        {
            _db.Errors.Add(error);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateError(Error error)
        {
            var currentError = _db.Errors.FirstOrDefault(e => e.ID == error.ID);
            if (currentError != null)
            {
                currentError.Date = error.Date;
                currentError.Description = error.Description;
                currentError.Title = error.Title;
                currentError.Status = error.Status;
                int result = await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteError(int Id)
        {
            var error = _db.Errors.FirstOrDefault(e => e.ID == Id);
            if (error != null)
            {
                _db.Remove(error);
                await _db.SaveChangesAsync();
            }
        }

    }

}