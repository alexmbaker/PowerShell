/********************************************************************++
Copyright (c) Microsoft Corporation.  All rights reserved.
--********************************************************************/
using System;
using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;

namespace System.Management.Automation.Internal
{
    /// <summary>
    /// Defines the parameters that are present on all Cmdlets.
    /// </summary>
    public sealed class CommonParameters
    {
        #region ctor

        /// <summary>
        /// Constructs an instance with the specified command instance
        /// </summary>
        /// 
        /// <param name="commandRuntime">
        /// The instance of the command that the parameters should set the
        /// user feedback properties on when the parameters get bound.
        /// </param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="cmdlet"/> is null.
        /// </exception>
        /// 
        internal CommonParameters (MshCommandRuntime commandRuntime)
        {
            if (commandRuntime == null)
            {
                throw PSTraceSource.NewArgumentNullException("commandRuntime");
            }
            this.commandRuntime = commandRuntime;
        } // ctor
        #endregion ctor

        static internal string[] CommonWorkflowParameters = { "PSComputerName", "JobName", "PSApplicationName", "PSCredential", "PSPort", "PSConfigurationName",
                                                              "PSConnectionURI", "PSSessionOption", "PSAuthentication", "PSAuthenticationLevel", "PSCertificateThumbprint",
                                                              "PSConnectionRetryCount", "PSConnectionRetryIntervalSec", "PSRunningTimeoutSec", "PSElapsedTimeoutSec",
                                                              "PSPersist", "PSPrivateMetadata", "InputObject", "PSParameterCollection",
                                                              "AsJob", "PSUseSSL", "PSAllowRedirection" };

#if !CORECLR // Workflow Not Supported On CSS
        internal static Type[] CommonWorkflowParameterTypes = {
             /* PSComputerName */ typeof(string[]),
             /* JobName */ typeof(string),
             /* PSApplicationName */ typeof(string),
             /* PSCredential */ typeof(PSCredential),
             /* PSPort */ typeof(uint),
             /* PSConfigurationName */ typeof(string),
             /* PSConnectionURI */ typeof(string[]),
             /* PSSessionOption */ typeof(PSSessionOption),
             /* PSAuthentication */ typeof(AuthenticationMechanism),
             /* PSAuthenticationLevel */ typeof(AuthenticationLevel),
             /* PSCertificateThumbprint */ typeof(string),
             /* PSConnectionRetryCount */ typeof(uint),
             /* PSConnectionRetryIntervalSec */ typeof(uint),
             /* ??? PSRunningTimeoutSec */ typeof(int),
             /* ??? PSElapsedTimeoutSec */ typeof(int),
             /* PSPersist */ typeof(bool),
             /* ??? PSPrivateMetadata */ typeof(object),
             /* ??? InputObject */ typeof(object),
             /* ??? PSParameterCollection */ typeof(Hashtable),
             /* AsJob */ typeof(bool),
             /* PSUseSSL */ typeof(bool),
             /* PSAllowRedirection */ typeof(bool),
        };
#endif

        #region parameters

        /// <summary>
        /// Gets or sets the value of the Verbose parameter for the cmdlet. 
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter
        /// tells the command to articulate the actions it performs while executing.
        /// </remarks>
        [Parameter]
        [Alias("vb")]
        public SwitchParameter Verbose
        {
            get { return commandRuntime.Verbose; }
            set { commandRuntime.Verbose = value; }
        } //Verbose

        /// <summary>
        /// Gets or sets the value of the Debug parameter for the cmdlet. 
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command to provide Programmer/Support type 
        /// messages to understand what is really occuring and give the user the 
        /// opportunity to stop or debug the situation.
        /// </remarks>
        [Parameter]
        [Alias("db")]
        public SwitchParameter Debug
        {
            get { return commandRuntime.Debug; }
            set { commandRuntime.Debug = value; }
        } //Debug

        /// <summary>
        /// Gets or sets the value of the ErrorAction parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command what to do when an error occurs.
        /// </remarks>
        [Parameter]
        [Alias("ea")]
        public ActionPreference ErrorAction
        {
            get { return commandRuntime.ErrorAction; }
            set { commandRuntime.ErrorAction = value; }
        } //ErrorAction

        /// <summary>
        /// Gets or sets the value of the WarningAction parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command what to do when a warning
        /// occurs.
        /// </remarks>
        [Parameter]
        [Alias("wa")]
        public ActionPreference WarningAction
        {
            get { return commandRuntime.WarningPreference; }
            set { commandRuntime.WarningPreference = value; }
        } //WarningAction

        /// <summary>
        /// Gets or sets the value of the InformationAction parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command what to do when an informational record occurs.
        /// </remarks>
        [Parameter]
        [Alias("infa")]
        public ActionPreference InformationAction
        {
            get { return commandRuntime.InformationPreference; }
            set { commandRuntime.InformationPreference = value; }
        } //InformationAction

        /// <summary>
        /// Gets or sets the value of the ErrorVariable parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command which variable to populate with the errors.
        /// Use +varname to append to the variable rather than clearing it.
        /// </remarks>
        /// 
        /// <!--
        /// 897599-2003/10/20-JonN Need to figure out how to get a working
        /// commandline parameter without making it a public property
        /// -->
        [Parameter]
        [Alias("ev")]
        [ValidateVariableName]
        public string ErrorVariable
        {
            get { return commandRuntime.ErrorVariable; }
            set { commandRuntime.ErrorVariable = value; }
        }//ErrorVariable

        /// <summary>
        /// Gets or sets the value of the WarningVariable parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command which variable to populate with the warnings.
        /// Use +varname to append to the variable rather than clearing it.
        /// </remarks>
        [Parameter]
        [Alias("wv")]
        [ValidateVariableName]
        public string WarningVariable
        {
            get { return commandRuntime.WarningVariable; }
            set { commandRuntime.WarningVariable = value; }
        }//WarningVariable

        /// <summary>
        /// Gets or sets the value of the InformationVariable parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command which variable to populate with the informational output.
        /// Use +varname to append to the variable rather than clearing it.
        /// </remarks>
        [Parameter]
        [Alias("iv")]
        [ValidateVariableName]
        public string InformationVariable
        {
            get { return commandRuntime.InformationVariable; }
            set { commandRuntime.InformationVariable = value; }
        }

        /// <summary>
        /// Gets or sets the OutVariable parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter tells the command to set all success output in the specified variable.
        /// Similar to the way -errorvariable sets all errors to a variable name.  
        /// Semantically this is equivalent to :  command |set-var varname -passthru
        /// but it should be MUCH faster as there is no binding that takes place
        /// </remarks>
        [Parameter]
        [Alias ("ov")]
        [ValidateVariableName]
        public string OutVariable
        {
            get { return commandRuntime.OutVariable; }
            set { commandRuntime.OutVariable = value; }
        } //OutVariable

        /// <summary>
        /// Gets or sets the OutBuffer parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter configures the number of objects to buffer before calling the downstream Cmdlet
        /// </remarks>
        [Parameter]
        [ValidateRangeAttribute(0, Int32.MaxValue)]
        [Alias("ob")]
        public int OutBuffer
        {
            get { return commandRuntime.OutBuffer; }
            set { commandRuntime.OutBuffer = value; }
        } //OutBuffer

        /// <summary>
        /// Gets or sets the PipelineVariable parameter for the cmdlet.
        /// </summary>
        /// 
        /// <remarks>
        /// This parameter defines a variable to hold the current pipeline output the command
        /// as it passes down the pipeline:
        /// Write-Output (1..10) -PipelineVariable WriteOutput | Foreach-Object { "Hello" }  |
        ///     Foreach-Object { $WriteOutput }
        /// </remarks>
        [Parameter]
        [Alias("pv")]
        [ValidateVariableName]
        public string PipelineVariable
        {
            get { return commandRuntime.PipelineVariable; }
            set { commandRuntime.PipelineVariable = value; }
        } //PipelineVariable

        #endregion parameters

        private MshCommandRuntime commandRuntime;

        internal class ValidateVariableName : ValidateArgumentsAttribute
        {
            protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
            {
                string varName = arguments as string;
                if (varName != null)
                {
                    if (varName.StartsWith("+", StringComparison.Ordinal))
                    {
                        varName = varName.Substring(1);
                    }
                    VariablePath silp = new VariablePath(varName);
                    if (!silp.IsVariable)
                    {
                        throw new ValidationMetadataException(
                            "ArgumentNotValidVariableName",
                            null,
                            Metadata.ValidateVariableName, varName);
                    }
                }
            }
        }
    } // class UserFeedbackParameters

}
