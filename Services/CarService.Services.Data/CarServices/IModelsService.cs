namespace CarService.Services.Data
{
    using System.Collections.Generic;

    public interface IModelsService
    {
        IEnumerable<T> GetAll<T>();

        string GetNameById(int id);
    }
}
