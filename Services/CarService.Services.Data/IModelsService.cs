namespace CarService.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IModelsService
    {
        IEnumerable<T> GetAll<T>();

        string GetNameById(int id);
    }
}
