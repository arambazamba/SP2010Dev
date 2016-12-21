using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

namespace Lab07.reviewContracts
{
    public sealed partial class reviewContracts : SequentialWorkflowActivity
    {
        public reviewContracts()
        {
            InitializeComponent();
        }

        

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        bool wfPending = true; 
        void checkDocStatus()
        {
            if (workflowProperties.Item["Contract Status"].ToString() == "Review Complete")
            {
                wfPending = false;
            }
        }
        private void onWorkflowActivated(object sender, ExternalDataEventArgs e)
        {
            //call our function
            checkDocStatus();
        }

        private void isWorkflowPending(object sender, ConditionalEventArgs e)
        {
            //set result to our class-level var
            e.Result = wfPending;
        }

        private void onWorkflowItemChanged(object sender, ExternalDataEventArgs e)
        {
            //call our function
            checkDocStatus();
        }

        //Add our function
       
    }
}
