using GameCore.Animal;
using GameCore.Animal.Interfaces;
using GameUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace GameCore.Locations
{
    public class Yard : MonoBehaviour
    {
        private IScoreManager _scoreManager;
        private IAnimalFactory _animalFactory;

        private void Start()
        {
            _scoreManager = DiContainer.Instance.ServiceProvider.GetRequiredService<IScoreManager>();
            _animalFactory = DiContainer.Instance.ServiceProvider.GetRequiredService<IAnimalFactory>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var animal = other.GetComponent<IAnimal>();
            if (animal == null) return;

            _scoreManager.IncreaseScore();
            _animalFactory.ReturnAnimal(animal);
        }
    }
}