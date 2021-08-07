using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.GameComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    class SpaceObjectGenerator : GameComponent
    {
        const int MAX_OBJECT_HEIGHT = 20;
        const float ASTEROID_INTERVAL = 0.5f;
        const float FUEL_INTERVAL = 6.0f;
        const float SPACESHIP_INTERVAL = 4.0f;

        Random random;
        GameScene parentScene;

        int numberOfAsteroids = 10;  // 100
        List<Asteroid> asteroids;      
        int asteroidIndex = 0;
        double asteroidIntervalTimer = 0;

        int numberOfFuelObjects = 1;  // 10
        List<FuelObject> fuelObjects;
        int fuelIndex = 0;
        double fuelIntervalTimer = 0;

        int numberOfSpaceShips = 1;  // 12
        List<SpaceShip> spaceShips;
        int spaceShipIndex = 0;
        double spaceShipIntervalTimer = 0;

        List<Planet> planets;
        int planetIndex = 0;
        

        public SpaceObjectGenerator(Game game, GameScene parent) 
            : base(game)
        {
            parentScene = parent;
            random = new Random();
            asteroids = new List<Asteroid>();
            fuelObjects = new List<FuelObject>();
            spaceShips = new List<SpaceShip>();
            planets = new List<Planet>();
        }

        public override void Initialize()
        {
            for (int a = 0; a < numberOfAsteroids; a++)
            {
                asteroids.Add(new Asteroid(Game, GetRandomAsteroidType(), CreateRandomPosition(), CreateRandomSpeed()));
            }

            for (int f = 0; f < numberOfFuelObjects; f++)
            {
                fuelObjects.Add(new FuelObject(Game, CreateRandomPosition()));
            }

            for (int s = 0; s < numberOfSpaceShips; s++)
            {
                spaceShips.Add(new SpaceShip(Game, GetRandomSpaceShipType(), CreateRandomPosition(), CreateRandomSpeed()));
            }

            planets.Add(new Planet(Game));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            asteroidIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (asteroidIndex < numberOfAsteroids)
            {
                if (asteroidIntervalTimer >= ASTEROID_INTERVAL)
                {
                    parentScene.AddComponent(asteroids[asteroidIndex]);
                    asteroidIndex++;

                    asteroidIntervalTimer = 0;
                }
            }

            fuelIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (fuelIndex < numberOfFuelObjects)
            {
                if (fuelIntervalTimer >= FUEL_INTERVAL)
                {
                    parentScene.AddComponent(fuelObjects[fuelIndex]);
                    fuelIndex++;

                    fuelIntervalTimer = 0;
                }
            }

            spaceShipIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spaceShipIndex < numberOfSpaceShips)
            {
                if (spaceShipIntervalTimer >= SPACESHIP_INTERVAL)
                {
                    parentScene.AddComponent(spaceShips[spaceShipIndex]);
                    spaceShipIndex++;

                    spaceShipIntervalTimer = 0;
                }
            }

            if (asteroidIndex >= numberOfAsteroids)
            {
                if (planetIndex == 0)
                {
                    parentScene.AddComponent(planets[planetIndex]);
                    planetIndex++;
                }               
            }

            base.Update(gameTime);
        }

        private AsteroidType GetRandomAsteroidType()
        {
            return (AsteroidType)random.Next(0, 4);
        }

        private SpaceShipType GetRandomSpaceShipType()
        {
            return (SpaceShipType)random.Next(0, 2);
        }

        private Vector2 CreateRandomPosition()
        {
            return new Vector2(Game.GraphicsDevice.Viewport.Width,
                random.Next(0, Game.GraphicsDevice.Viewport.Height - MAX_OBJECT_HEIGHT));
        }

        private int CreateRandomSpeed()
        {
            return random.Next(5, 10);
        }
    }
}
