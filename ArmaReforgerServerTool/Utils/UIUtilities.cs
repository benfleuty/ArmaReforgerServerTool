/******************************************************************************
 * File Name:    Utilities.cs
 * Project:      Arma Reforger Dedicated Server Tool for Windows
 * Description:  Static class containing utility methods for 
 *               performing various simple tasks
 * 
 * Author:       Bradley Newman
 ******************************************************************************/

using Serilog;

namespace ReforgerServerApp.Utils
{
  /// <summary>
  /// Static class containing utility methods for performing various simple tasks
  /// </summary>
  internal class UIUtilities
  {
    /// <summary>
    /// Convenience method for Displaying an Error Messagebox
    /// </summary>
    /// <param name="genMsg">General info about the error</param>
    /// <param name="errMsg">detailed message from the exception, if applicable</param>
    public static void DisplayErrorMessage(string genMsg, string errMsg)
    {
      Log.Error("An error prompt was displayed: {genMsg} - {errMsg}", genMsg, errMsg);
      MessageBox.Show(
          $"{genMsg}" + Environment.NewLine +
          $"Detail: {errMsg}" + Environment.NewLine +
          $"Include the detail above in your bug reports.",
          Constants.ERROR_MESSAGEBOX_TITLE_STR);
    }

    /// <summary>
    /// Convenience method for Displaying a Confirmation Messagebox 
    /// (message box with OK and Cancel buttons, or Yes and No if useYesOrNo = true)
    /// </summary>
    /// <param name="msg">Warning message to display</param>
    /// <param name="useYesOrNo">Use Yes or No buttons instead of OK and Cancel</param>
    /// <returns>True if the following logic should continue, False otherwise</returns>
    public static bool DisplayConfirmationMessage(string msg, bool useYesOrNo = false)
    {
      MessageBoxButtons buttons = useYesOrNo ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel;
      DialogResult result = MessageBox.Show($"{msg}", Constants.WARN_MESSAGEBOX_TITLE_STR, buttons);
      return result == DialogResult.OK || result == DialogResult.Yes;
    }
  }
}
