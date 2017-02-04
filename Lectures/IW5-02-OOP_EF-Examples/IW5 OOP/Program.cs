using System;
using System.Collections;
using System.Collections.Generic;
using IW5_OOP.Generics;
using IW5_OOP.Pets;
using IW5_OOP.Stacks;

namespace IW5_OOP
{
    internal static class Program
    {
        private static void ConstructorInitialization()
        {
            var initCat = new ConstructorInitializationExample.Cat("Chalsie", 5) { Age = 5 };
        }

        private static void TypeCompatibility()
        {
            var dog1 = new Dog("Punta");
            Pet animal1 = dog1; // Upcast
            var dog2 = (Dog) animal1; // Downcast

            if (dog2 == animal1)
                Console.WriteLine("dog2 == animal1, true");

            if (dog2 == dog1)
                Console.WriteLine("dog2 == dog1, true");


            var cat1 = new Cat("Chalsie");

            // Upcast always succeeds
            Pet pet = cat1;
            pet = new Dog("Alik");

            //Downcast fails:a2 is not a Cat
            try
            {
                var dog3 = (Cat) pet; //throws InvalidCastException
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void VirtualMethods()
        {
            Console.WriteLine("Choose 1) Cat 2) Dog?");
            var selectAnimal = Console.ReadLine();

            Pet pet;
            if (selectAnimal == "1")
            {
                pet = new Cat("Chalsie");
            }
            else
            {
                pet = new Dog("Alik");
            }

            pet.Draw();
        }

        private static void IsAsOperators()
        {
            var a = new Pet();
            Console.WriteLine(a is Dog ? "a is a Dog" : "a is not a Dog");

            var dog = a as Dog; //Dog == null 
            Console.WriteLine(dog != null ? "dog is a Dog" : "dog is not a Dog");
        }

        private static void Enums()
        {
            var horse = new Horse();

            horse.Color = Horse.HorseColor.Ryzak;
            Console.WriteLine(horse.Color);

            horse.SetColor(0);
            Console.WriteLine(horse.Color);

            horse.SetColor("Palomino");
            Console.WriteLine(horse.Color);

            Console.WriteLine(horse.Type);
            horse.Type = Horse.HorseType.Racing;
            horse.Type |= Horse.HorseType.Breeding;
            horse.Type |= Horse.HorseType.ForSosages;
            Console.WriteLine(horse.Type);
        }

        private static void StackExample()
        {
            var intStack = new IntStack();
            intStack.Push(111);
            var a = intStack.Pop();
            Console.WriteLine("Integer stack: " + a);

            var objectStack = new ObjectStack();
            objectStack.Push(111); //boxing
            var o = objectStack.Pop();
            Console.WriteLine("Object stack: " + o);

            var commonStack = new CommonStack<int>();
            commonStack.Push(111); //boxing
            var s = commonStack.Pop();
            Console.WriteLine("Common stack: " + s);
        }

        private static void GenericsExample()
        {
            var listTextValue = new List<TextValue<string, int>>();

            for (var k = 0; k < 5; ++k)
            {
                var textValue = new TextValue<string, int>("", 0)
                {
                    Text = "Item " + k,
                    Value = k
                };
                listTextValue.Add(textValue);
            }

            foreach (var item in listTextValue)
            {
                Console.WriteLine("Text: {0}, Value: {1}", item.Text, item.Value);
            }
        }

        private static void Properties()
        {
            Console.WriteLine("Simple Properties");

            // Create a new Animal object:
            var cat = new Cat("Chalsie");

            // Print out the name and the age associated with the Animal:
            Console.WriteLine("Cat`s details - {0}", cat);

            // Set some values on the Animal object:
            cat.Age = 19;
            Console.WriteLine("Cat`s details - {0}", cat);

            // Increment the Age property:
            cat.Age = -1;
            Console.WriteLine("Cat`s details - {0}", cat);
        }

        private static void Interfaces()
        {
            Console.WriteLine();
            Console.WriteLine("Cat:");
            var cat = new Cat("Chalsie");
            Console.WriteLine($"Cat - {cat.NickName}");
            cat.Draw();

            Console.WriteLine();
            Console.WriteLine("INickName:");
            INickName nickName = cat;
            Console.WriteLine($"INickName - {nickName.NickName}");

            Console.WriteLine();
            Console.WriteLine("IDraw:");
            IDraw drawAbleCat = cat;
            drawAbleCat.Draw();

            Console.WriteLine();
            Console.WriteLine("IDrawableNickname:");
            IDrawableNickname drawAbleNickNamedCat = cat;
            Console.WriteLine($"IDrawableNickname - {drawAbleNickNamedCat.NickName}");
            drawAbleNickNamedCat.Draw();

            //Example of an implicit implementation is in the Storage class
        }

        /// <summary>
        /// https://blogs.msdn.microsoft.com/csharpfaq/2010/02/16/covariance-and-contravariance-faq/
        /// </summary>
        private static void Variant()
        {
            // Assignment compatibility.
            string str = "test";
            // An object of a more derived type is assigned to an object of a less derived type.
            object obj = str;

            // Covariance.
            IEnumerable<string> strings = new List<string>();
            // An object that is instantiated with a more derived type argument
            // is assigned to an object instantiated with a less derived type argument.
            // Assignment compatibility is preserved.
            IEnumerable<object> objects = strings;

            // Contravariance.           
            // Assume that I have this method:
            Action<object> actObject = SetObject;
            // An object that is instantiated with a less derived type argument
            // is assigned to an object instantiated with a more derived type argument.
            // Assignment compatibility is reversed.
            Action<string> actString = actObject;
            actString("11");

            //Invariance
            Storage<Pet> storage = new Storage<Pet>();
            IRetreiver<Pet> retreiverPet = storage;
            IDepositor<Pet> depositorPet = storage;

            //Contravariance
            IDepositor<Dog> depositorDog = storage;
            //Covariance
            IRetreiver<Animal> retreiverAnimal = storage;
        }

        static void SetObject(object o)
        {
        }

        static class PartialClass
        {
            // Dog1Gen.cs - auto-generated
            partial class Dog1Form
            {
                public Dog1Form()
                {
                    this.Bark();
                }

                partial void Bark();
            }

            // Dog1Form.cs - hand-authored
            partial class Dog1Form
            {
                partial void Bark()
                {
                    Console.WriteLine("Bark");
                }
            }
        }

        private static void Main(string[] args)
        {
            ConstructorInitialization();
            Properties();
            TypeCompatibility();
            VirtualMethods();
            IsAsOperators();
            Enums();
            StackExample();
            GenericsExample();
            Interfaces();
            Variant();

            ////PartialClass
            Console.ReadKey();
        }
    }
}