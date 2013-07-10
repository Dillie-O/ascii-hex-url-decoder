using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace URL_Decoder
{
   /// <summary>
   /// Interaction logic for Window1.xaml
   /// </summary>
   public partial class Main : Window
   {
      public Main()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Exits the application.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <history>
      ///    [Sean Patterson]   8/1/2008   Created
      /// </history>
      private void QuitApplication(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      /// <summary>
      ///    Shows a basic about page.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <history>
      ///    [Sean Patterson]   8/1/2008   Created
      /// </history> 
      private void AboutApplication(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Ascii Hex URL Decoder" + Environment.NewLine + Environment.NewLine +
                         "Version 1.0" + Environment.NewLine +
                         "Dillie-O Digital" + Environment.NewLine + Environment.NewLine +
                         "Decoder for Ascii/Binary data used in SQL injection attacks.", "About Application", 
                         MessageBoxButton.OK, MessageBoxImage.Information);
      }

      /// <summary>
      ///    Decodes the URL specified into plain text.
      /// </summary>
      /// <param name="sender">Decode Button Click Event.</param>
      /// <param name="e">Button Click Event Arguments.</param>
      /// <history>
      ///    [Sean Patterson]   8/1/2008   Created
      /// </history>
      private void DecodeURL(object sender, RoutedEventArgs e)
      {
         string strDecodedData;
         string strEncodedData;
         Regex rgxEncodedData = new Regex("(.*CAST\\()(0x)(\\w+)(.*)");
         Match mchEncodedData;

         mchEncodedData = rgxEncodedData.Match(txtURLText.Text.Trim());

         if (mchEncodedData.Success)
         {
            strEncodedData = mchEncodedData.Groups[3].Value;
            strDecodedData = ConvertAsciiHexToString(strEncodedData);

            if (Convert.ToBoolean(radResultsValue.IsChecked))
            {
               txtDecodedText.Text = strDecodedData;
            }
            else
            {
               txtDecodedText.Text = mchEncodedData.Groups[1].Value + strDecodedData + mchEncodedData.Groups[4].Value;
            }
            
         }
         else
         {
            MessageBox.Show("No encoded data found." + Environment.NewLine + Environment.NewLine +
                            "Make sure the data to decode is wrapped in a 'CAST(0x.... AS VARCHAR)' statement", 
                            "No data to decode.", MessageBoxButton.OK, MessageBoxImage.Warning);
         }
                  
      }
      
      /// <summary>
      ///    Converts data encoded in ASCII Hex to a String.
      /// </summary>
      /// <param name="EncodedString">Encoded data.</param>
      /// <returns>String with decoded data.</returns>
      /// <remarks>
      ///    This method assumes that the encoded string is using 2 characters per hex code. The method will do the proper
      ///    double conversion to get the string into a proper hexadecimal value before converting the character to a string.
      /// </remarks>
      /// <history>
      ///    [Sean Patterson]   8/1/2008   Created
      /// </history>
      private string ConvertAsciiHexToString(string EncodedString)
      {
         int intParseIndex = 0;
         bool boolIsDone = false;
         StringBuilder sbResults = new StringBuilder();
        
         while (!boolIsDone)
         {
            sbResults.Append(Convert.ToString(Convert.ToChar(Int32.Parse(EncodedString.Substring(intParseIndex, 2), 
                             System.Globalization.NumberStyles.HexNumber))));

            intParseIndex += 2;

            if (EncodedString.Length - intParseIndex < 2)
            {
               boolIsDone = true;
            }
         }

         return sbResults.ToString();
      }

   }
}
