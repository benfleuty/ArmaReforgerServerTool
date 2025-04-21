/******************************************************************************
 * File Name:    Utilities.cs
 * Project:      Arma Reforger Dedicated Server Tool for Windows
 * Description:  Static class containing utility methods for
 *               performing various simple tasks
 *
 * Author:       Bradley Newman
 ******************************************************************************/

using Serilog;
using System.ComponentModel;

namespace ReforgerServerApp.Utils
{
  /// <summary>
  /// Static class containing utility methods for performing various simple tasks
  /// </summary>
  internal class UIUtilities
  {
    /// <summary>
    /// Moves an item in a list forward or backward.
    /// </summary>
    /// <param name="list">The list of items.</param>
    /// <param name="item">The item to move.</param>
    /// <param name="moveBackward">Optional, moves forward by default, set to true to move it backward.</param>
    public static void MoveItem<T>(BindingList<T> list, T item, bool moveBackward = false)
    {
      if (list == null)
      {
        // List is null, don't do anything
        return;
      }

      int index = list.IndexOf(item);

      if (index == -1)
      {
        // Item doesn't exist in the list, don't do anything
        return;
      }

      int newIndex = moveBackward ? index - 1 : index + 1;

      if (newIndex < 0 || newIndex >= list.Count)
      {
        // Can't move outside list bounds, do nothing
        return;
      }

      // Swap the items
      (list[newIndex], list[index]) = (list[index], list[newIndex]);
    }

    /// <summary>
    /// Convenience method for Displaying an Error Messagebox
    /// </summary>
    /// <param name="genMsg">General info about the error</param>
    /// <param name="errMsg">detailed message from the exception, if applicable</param>
    public static void DisplayErrorMessage(string genMsg, string errMsg)
    {
      Log.Error("An error prompt was displayed: {genMsg} - {errMsg}", genMsg, errMsg);
      string msg =
          $"{genMsg}" + Environment.NewLine +
          $"Detail: {errMsg}" + Environment.NewLine +
          "Include the detail above in your bug reports.",
      MessageBox.Show(msg, Constants.ERROR_MESSAGEBOX_TITLE_STR);
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
      DialogResult result = MessageBox.Show(msg, Constants.WARN_MESSAGEBOX_TITLE_STR, buttons);
      return result == DialogResult.OK || result == DialogResult.Yes;
    }
  }
}
