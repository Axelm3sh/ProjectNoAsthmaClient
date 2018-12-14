using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoAsthmaClientApp.Source.API.Helper
{
    //FormHandler
    public static class FormHandler
    {
        public static List<Form> FormsList { get; set; }

        static FormHandler() //Static constructor
        {
            FormsList = new List<Form>();
        }

        public static int CountAvailableForms()
        {
            return FormsList.Count();
        }

        /// <summary>
        /// Function wrapper for Close()
        /// </summary>
        /// <param name="formName">the form name we are searching for</param>
        /// <returns>returns if search was successful, if it was also close the form</returns>
        public static bool CloseForm(string formName)
        {
            bool searchSuccess = false;
            foreach (Form finder in FormsList)
            {
                if (finder.Name == formName)
                {
                    //finder.Close();

                    //Remove reference to prevent accessing any further
                    FormsList.Remove(finder);

                    searchSuccess = true;

                    break;
                }
            }
            return searchSuccess;
        }
        
        //Private method for internal clean up
        private static void FormCloseEvent(object sender, FormClosingEventArgs e)
        {
            //Try casting over sender object as a Form, if cast fails, null returned
            Form formName = sender as Form;

            //Check for null objects
            if(formName != null)
            {
                //Check if form is in middle of disposal
                if (!formName.Disposing)
                {
                    //Close form
                    CloseForm(formName.Name);
                }
            }
            else
            {
                //e.Cancel = true;
                //Opt for a message box
                DialogResult result = MessageBox.Show("HANDLER: Cannot find Form!", 
                    "ERROR: Null Object", 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                //Switch statement for different results
                switch (result)
                {
                    case DialogResult.OK:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                }//END SWITCH
            }
            
        }

        /// <summary>
        /// Creates the new form and returns a reference to the newly created form
        /// </summary>
        /// <param name="newForm">The new form that you created</param>
        /// <returns>The form that was just created</returns>
        public static Form CreateForm(Form newForm, bool show = true)
        {
            //Add the new form to the list
            FormsList.Add(newForm);

            //If we want to show it
            if (show)
            {
                newForm.Show();
            }

            //Bind this new form's closing event to our clean up
            newForm.FormClosing += new FormClosingEventHandler(FormCloseEvent);

            return newForm;
        }

    }
}
