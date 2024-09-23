﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace TankBattle
{
    public class PlayerBullet : DrawableGameComponent
    {
        private Texture2D texture; //Texture de la munition
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _BulletPosition;

        //Bullet properties
        private Vector2 _Velocity;
        private const float _SPEED = 35;
        private const int _DIRECTION = -1;

        public PlayerBullet(Game game, Vector2 playerPosition) : base(game) 
        {
            /*Calculer la position du canon, car la position du joueur se trouve au centre du sprite, alors que 
             * la sortie du canon est positionnée ailleurs. Cependant il n'existe aucun calcul me permettant de trouver la position. 
            J'ai donc du calculer à taton*/
            _BulletPosition = new Vector2(playerPosition.X + 14,playerPosition.Y -68);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger le sprite de la munition
            texture = Game.Content.Load<Texture2D>("PlayerBullet");

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _Velocity.Y = _SPEED * _DIRECTION;
            _BulletPosition += _Velocity;
            //Regarder si la balle sort de l'écran et la supprimer si oui.
            if (IsOutOfBounds())
                Game.Components.Remove(this);
            CheckCollisions();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, _BulletPosition, null, Color.White, -MathHelper.PiOver2, new Vector2(texture.Width / 2, texture.Height / 2), 0.15f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        private bool IsOutOfBounds() => _BulletPosition.Y < 0;
        private void CheckCollisions()
        {
            //Créer une liste qui contiendra les tanks a supprimer, car je ne peux pas le supprimer directement, sinon cela produit une erreur
            List<Ennemy> TankToRemove = new List<Ennemy>();

            foreach (Ennemy tank in GameRoot.Tanks)
            {
                if (_BulletPosition.Y < tank.EnnemyPosition.Y + (tank.texture.Height * 1f) / 2 &&
                    _BulletPosition.Y > tank.EnnemyPosition.Y - (tank.texture.Height * 1f) / 2 &&
                    _BulletPosition.X > tank.EnnemyPosition.X - (tank.texture.Width * 1f) / 2 &&
                    _BulletPosition.X < tank.EnnemyPosition.X + (tank.texture.Width * 1f) / 2)
                {
                    //Supprimer le tank
                    Game.Components.Remove(tank);
                    //Ajouter le tank à la liste de suppression
                    TankToRemove.Add(tank);
                }
            }
            foreach(Ennemy DeadTank in TankToRemove)
            {
                GameRoot.Tanks.Remove(DeadTank);
            }
        }
    }
}