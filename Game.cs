using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame
{

    static class Game
    {
        private const int FramesPerSecond = 60;

        private static Scene _currentScene;
        private static Scene _nextScene;

        private static readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();

        private static readonly Dictionary<string, SoundBuffer> Sounds = new Dictionary<string, SoundBuffer>();

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        private static RenderWindow _window;

        private static bool _initialized;


        public static Random Random = new Random(42);

        public static void Initialize(uint windowWidth, uint windowHeight, string windowTitle)
        {
            if (_initialized) return;
            _initialized = true;

            _window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            _window.SetFramerateLimit(FramesPerSecond);

            _window.Closed += ClosedEventHandler;
        }

        private static void ClosedEventHandler(object sender, EventArgs e)
        {
            _window.Close();
        }

        public static RenderWindow RenderWindow
        {
            get { return _window; }
        }
        public static Texture GetTexture(string fileName)
        {
            Texture texture;

            if (Textures.TryGetValue(fileName, out texture)) return texture;

            texture = new Texture(fileName);
            Textures[fileName] = texture;
            return texture;
        }

        public static SoundBuffer GetSoundBuffer(string fileName)
        {
            SoundBuffer soundBuffer;

            if (Sounds.TryGetValue(fileName, out soundBuffer)) return soundBuffer;

            soundBuffer = new SoundBuffer(fileName);
            Sounds[fileName] = soundBuffer;
            return soundBuffer;
        }
        public static Font GetFont(string fileName)
        {
            Font font;

            if (Fonts.TryGetValue(fileName, out font)) return font;

            font = new Font(fileName);
            Fonts[fileName] = font;
            return font;
        }

        public static Scene CurrentScene
        {
            get { return _currentScene; }
        }

        public static void SetScene(Scene scene)
        {
            if (_currentScene == null)
                _currentScene = scene;
            else
                _nextScene = scene;
        }

        public static void Run()
        {
            Clock clock = new Clock();
            while (_window.IsOpen)
            {
                if (_nextScene != null)
                {
                    _currentScene = _nextScene;
                    _nextScene = null;
                    clock.Restart();
                }

                Time time = clock.Restart();
                _currentScene.Update(time);
            }
        }
    }
}