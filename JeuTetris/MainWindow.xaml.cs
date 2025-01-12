using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages;
        private readonly ImageSource[] blockImages;
        private readonly Image[,] imageControls;
        private GameState gameState;
        private bool isPaused;
        private int gameSpeed = 500;

        public MainWindow()
        {
            InitializeComponent();

            // Chargement des images
            tileImages = LoadTileImages();
            blockImages = LoadBlockImages();

            // Initialisation du jeu
            gameState = new GameState();
            imageControls = SetupGameCanvas(gameState.GameGrid);
            ShowMainMenu();
        }

        private ImageSource[] LoadTileImages()
        {
            return new ImageSource[]
            {
                new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
            };
        }

        private ImageSource[] LoadBlockImages()
        {
            return new ImageSource[]
            {
                new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
            };
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = 25,
                        Height = 25
                    };
                    Canvas.SetTop(imageControl, r * 25);
                    Canvas.SetLeft(imageControl, c * 25);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                if (!isPaused)
                {
                    await Task.Delay(gameSpeed);
                    gameState.MoveBlockDown();
                    Draw(gameState);
                }
                else
                {
                    await Task.Delay(100);
                }
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            GameCanvas.Visibility = Visibility.Visible;
            ScoreDisplay.Visibility = Visibility.Visible;
            NextBlockDisplay.Visibility = Visibility.Visible;
            gameState = new GameState();
            _ = GameLoop();
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            isPaused = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.P && !gameState.GameOver)
            {
                isPaused = !isPaused;
                PauseMenu.Visibility = isPaused ? Visibility.Visible : Visibility.Collapsed;
            }

            if (isPaused || gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Space:
                    gameState.DropBlock();
                    break;
            }

            Draw(gameState);
        }

        private void ShowMainMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Collapsed;
            ScoreDisplay.Visibility = Visibility.Collapsed;
            NextBlockDisplay.Visibility = Visibility.Collapsed;
            PauseMenu.Visibility = Visibility.Collapsed;
            GameOverMenu.Visibility = Visibility.Collapsed;
        }

        private void OpenOptionsMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            OptionsMenu.Visibility = Visibility.Visible;
        }

        private void ApplySettings_Click(object sender, RoutedEventArgs e)
        {
            gameSpeed = (int)GameSpeedSlider.Value;
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            OptionsMenu.Visibility = Visibility.Collapsed;
            GameOverMenu.Visibility = Visibility.Collapsed;
            ShowMainMenu();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Collapsed;
            gameState = new GameState();
            _ = GameLoop();
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            NextImage.Source = blockImages[blockQueue.NextBlock.Id];
        }

        private void DrawHeldBlock(Block heldBlock)
        {
            // Méthode optionnelle
        }
    }
}

