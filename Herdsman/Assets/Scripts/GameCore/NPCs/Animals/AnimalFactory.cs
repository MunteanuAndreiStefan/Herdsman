using System;
using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using Random = UnityEngine.Random;


namespace GameCore.Animal
{
    /// <summary>
    /// Open/close principle
    /// Single-Responsibility Principle
    /// </summary>
    public class AnimalFactory : IAnimalFactory
    {
        private readonly Dictionary<string, Func<IAnimal>> _animalFactories = new();
        private readonly Dictionary<string, Action<IAnimal>> _animalReturnMethods = new();
        private List<string> _animals = new List<string>();

        public IAnimal CreateAnimal(string type)
        {
            if (_animalFactories.TryGetValue(type, value: out var factory))
                return factory();

            throw new ArgumentException("Invalid animal type");
        }

        public IAnimal CreateRandomAnimal()
        {
            var randomAnimal = Random.Range(0, _animals.Count);
            if (_animalFactories.TryGetValue(_animals[randomAnimal], value: out var factory))
                return factory();

            throw new ArgumentException("Something went wrong with animal types!");
        }

        public void ReturnAnimal(IAnimal animal)
        {
            var type = animal.GetType().Name;
            if (_animalReturnMethods.TryGetValue(type, out var method))
            {
                method(animal);
            }
            else
            {
                throw new ArgumentException("No return registered for this animal type!");
            }
        }

        public void RegisterAnimal(string type, Func<IAnimal> factoryMethod, Action<IAnimal> returnMethod)
        {
            if (_animalFactories.TryAdd(type, factoryMethod))
            {
                _animalReturnMethods[type] = returnMethod;
                _animals = new List<string>(_animalFactories.Keys);
            }
            else
            {
                throw new ArgumentException($"Animal type {type} duplicate!");
            }
        }
    }
}