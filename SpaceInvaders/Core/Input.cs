﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Entity;

namespace SpaceInvaders.Core
{
    public static class Input
    {
        private static KeyboardState _keyboardState, _lastKeyboardState;
        private static MouseState _mouseState, _lastMouseState;
        private static GamePadState _gamepadState, _lastGamepadState;

        private static bool isAimingWithMouse;

        public static Vector2 MousePosition => new Vector2(_mouseState.X, _mouseState.Y);

        public static void Update()
        {
            _lastKeyboardState = _keyboardState;
            _lastMouseState = _mouseState;
            _lastGamepadState = _gamepadState;

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _gamepadState = GamePad.GetState(PlayerIndex.One);

            if (new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down }.Any(x => _keyboardState.IsKeyDown(x)) ||
                _gamepadState.ThumbSticks.Right != Vector2.Zero)
                isAimingWithMouse = false;
            else if (MousePosition != new Vector2(_lastMouseState.X, _lastMouseState.Y))
                isAimingWithMouse = true;
        }

        // Checks if a key was just pressed down
        public static bool WasKeyPressed(Keys key)
        {
            return _lastKeyboardState.IsKeyUp(key) && _keyboardState.IsKeyDown(key);
        }

        public static bool WasButtonPressed(Buttons button)
        {
            return _lastGamepadState.IsButtonUp(button) && _gamepadState.IsButtonDown(button);
        }

        public static Vector2 GetMovementDirection()
        {
            var direction = _gamepadState.ThumbSticks.Left;
            direction.Y *= -1; // invert the y-axis

            if (_keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (_keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (_keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (_keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        public static Vector2 GetMovementInput()
        {
            var direction = _gamepadState.ThumbSticks.Left;
            direction.Y *= -1; // invert the y-axis
            if (_keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (_keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;

            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        public static Vector2 GetAimDirection()
        {
            if (isAimingWithMouse)
                return GetMouseAimDirection();

            var direction = _gamepadState.ThumbSticks.Right;
            direction.Y *= -1;

            if (_keyboardState.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (_keyboardState.IsKeyDown(Keys.Right))
                direction.X += 1;
            if (_keyboardState.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (_keyboardState.IsKeyDown(Keys.Down))
                direction.Y += 1;

            // If there's no aim input, return zero. Otherwise normalize the direction to have a length of 1.
            if (direction == Vector2.Zero)
                return Vector2.Zero;
            return Vector2.Normalize(direction);
        }

        private static Vector2 GetMouseAimDirection()
        {
            var direction = MousePosition - PlayerShip.Instance.Position;

            if (direction == Vector2.Zero)
                return Vector2.Zero;
            return Vector2.Normalize(direction);
        }

        public static bool WasBombButtonPressed()
        {
            return WasButtonPressed(Buttons.LeftTrigger) || WasButtonPressed(Buttons.RightTrigger) ||
                   WasKeyPressed(Keys.Space);
        }

        public static bool WasFireKeyPressed()
        {
            return WasKeyPressed(Keys.Space);
        }

        public static bool WasRocketKeyPressed()
        {
            return WasKeyPressed(Keys.D1);
        }

        public static bool WasBombKeyPressed()
        {
            return WasKeyPressed(Keys.D2);
        }

        public static bool WasLaserKeyPressed()
        {
            return WasKeyPressed(Keys.D3);
        }

        public static bool WasUpgradeButtonPressed()
        {
            return WasKeyPressed(Keys.T);
        }
    }
}