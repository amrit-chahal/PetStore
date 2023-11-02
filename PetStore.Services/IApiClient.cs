using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface IApiClient
    {
        Task<List<PetStoreModel>> GetPetsAsync(string endPoint);
    }
}
