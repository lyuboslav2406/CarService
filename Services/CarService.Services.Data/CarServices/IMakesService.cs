namespace CarService.Services.Data
{
    using System.Collections.Generic;

    public interface IMakesService
    {
        IEnumerable<T> GetAll<T>();

        string GetNameById(int id);
    }
}
