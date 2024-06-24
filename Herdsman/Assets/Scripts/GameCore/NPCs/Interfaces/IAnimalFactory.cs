using System;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalFactory
    {
        IAnimal CreateAnimal(string type);
        void ReturnAnimal(IAnimal animal);
        
        void RegisterAnimal(string type, Func<IAnimal> factoryMethod, Action<IAnimal> returnMethod);
        IAnimal CreateRandomAnimal();
    }
}