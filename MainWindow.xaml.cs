using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Animals_dont_match_on_Apps
{
   //adds DispatcherTimer
   using System.Windows.Threading;
   public partial class MainWindow : Window
   {
      DispatcherTimer timer = new DispatcherTimer();
      int tenthsOfSecondsElapsed;
      int matchesFound;
      public MainWindow()
      {
         InitializeComponent();

         timer.Interval = TimeSpan.FromSeconds(.1);
         timer.Tick += Timer_Tick;

         SetUpGame();

      }

      private void Timer_Tick(object sender, EventArgs e)
      {
         tenthsOfSecondsElapsed++;
         timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
         //tells timer when to stop
         if (matchesFound == 8)
         {
            timer.Stop();
            timeTextBlock.Text += " - Again?";
         }
      }

      public void SetUpGame()
      {         
         List<string> animalEmoji = new List<string>()
         {
            "🎃","🎃",
            "🦈","🦈",
            "🐲","🐲",
            "🐸","🐸",
            "🐗","🐗",
            "🐙","🐙",
            "🐔","🐔",
            "🐍","🐍",
         };
         Random random = new Random();
         
         foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
         {
            //excluding timeTextBlock
            if (textBlock.Name != "timeTextBlock")
            {
               //randomly assigning one emoji to one TextBlock per loop
               //also making all TextBlocks visible
               textBlock.Visibility = Visibility.Visible;
               //chooses Emoji based on it's Index
               int index = random.Next(animalEmoji.Count);
               string nextEmoji = animalEmoji[index];
               //assign Emoji to textblock
               textBlock.Text = nextEmoji;
               //removes Emoji from Index
               animalEmoji.RemoveAt(index);
            }
            
         }
         timer.Start();
         tenthsOfSecondsElapsed = 0;
         matchesFound = 0;
      }
      TextBlock lastTextBlockClicked;
      bool findingMatch = false;
      private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         TextBlock textBlock = sender as TextBlock;
         //checks if match is made
         if (findingMatch == false)
         {
            //makes textBlock invisible once it's clicked
            textBlock.Visibility = Visibility.Hidden;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
         }
         else if (textBlock.Text == lastTextBlockClicked.Text)
         {
            //adds +1 value to matchesFound when a match is made            
            matchesFound++;
            //keeps match textBlocks hidden
            textBlock.Visibility = Visibility.Hidden;
            //proceeds to else when no match is being made
            findingMatch = false;
         }
         else
         {
            //makes wrongly selected textBlocks visible again
            lastTextBlockClicked.Visibility = Visibility.Visible;
            findingMatch = false;
         }

      }

      private void TimeTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         //jumps back to SetUpGame after finishing the game - resetting it
         //triggered by clicking timeTextBlock once all matches are found
         if (matchesFound == 8 )
         {
            SetUpGame();
         }
      }
   }
}
