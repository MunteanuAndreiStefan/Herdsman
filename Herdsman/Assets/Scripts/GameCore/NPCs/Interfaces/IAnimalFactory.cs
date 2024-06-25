﻿using System;

namespace GameCore.Animal.Interfaces
{
    public interface IAnimalFactory
    {
        public IAnimal CreateAnimal(string type);
        public void ReturnAnimal(IAnimal animal);
        
        public void RegisterAnimal(string type, Func<IAnimal> factoryMethod, Action<IAnimal> returnMethod);
        public IAnimal CreateRandomAnimal();
    }
}