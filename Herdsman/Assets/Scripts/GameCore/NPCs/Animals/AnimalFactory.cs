using System;
using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using Random = UnityEngine.Random;

namespace GameCore.Animal
{
    /// <summary>
    /// Animal factory that creates and returns animals back to object pools.
    /// Solid example of:
    ///  - Open/close principle
    ///  - Single-Responsibility Principle
    /// Design pattern example of:
    ///  - Factory pattern
    /// </summary>
    public class AnimalFactory : IAnimalFactory
    {
        private readonly Dictionary<string, Func<IAnimal>> _animalFactories = new();
        private readonly Dictionary<string, Action<IAnimal>> _animalReturnMethods = new();
        private List<string> _animals = new List<string>();

        /// <summary>
        /// Creates an animal based on the type.
        /// </summary>
        /// <param name="type">String value represeting the type of animal wanted.</param>
        /// <returns>IAnimal implementation of a animal.</returns>
        /// <exception cref="ArgumentException">Error: "Invalid animal type", if type doesn't exist</exception>
        public IAnimal CreateAnimal(string type)
        {
            if (_animalFactories.TryGetValue(type, value: out var factory))
                return factory();

            throw new ArgumentException("Invalid animal type");
        }

        /// <summary>
        /// Creates a random animal
        /// </summary>
        /// <returns>IAnimal implementation of a animal.</returns>
        /// <exception cref="ArgumentException">If animals are not existent returns error: "Something went wrong with animal types!" </exception>
        public IAnimal CreateRandomAnimal()
        {
            var randomAnimal = Random.Range(0, _animals.Count);
            if (_animalFactories.TryGetValue(_animals[randomAnimal], value: out var factory))
                return factory();

            throw new ArgumentException("Something went wrong with animal types!");
        }
        
        /// <summary>
        /// Returns an animal back to the object pool.
        /// </summary>
        /// <param name="animal">IAnimal implementation wanted to get disposed.</param>
        /// <exception cref="ArgumentException">Returns: "No return registered for this animal type!", if operation can't be done</exception>
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

        /// <summary>
        /// Register an animal type with a factory method and a return method.
        /// </summary>
        /// <param name="type">String value of the animal type defined</param>
        /// <param name="factoryMethod">Build method for animal</param>
        /// <param name="returnMethod">Dispose method for animal</param>
        /// <exception cref="ArgumentException">If the type already exists returns: $"Animal type {type} duplicate!"</exception>
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