using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBotPlugin;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace TableListCommands
{
    public partial class TableAddonCommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public TableAddonCommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            var HeaderYesNo = new UBotParameterDefinition("Contains Headers?", UBotType.String);
            HeaderYesNo.Options = new[] {"Yes", "No" };
            _parameters.Add(HeaderYesNo);
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Window Title", UBotType.String));

        }

        public string FunctionName
        {
            get { return "$edit tabular csv"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            //string contentName = parameters["csvcontent"];

            string value = parameters["csvcontent"]; // ubotStudio.GetVariable(contentName);

            string containsHeader = parameters["Contains Headers?"];

            string delimiter = parameters["Delimiter"];

            string title = parameters["Window Title"];
            
            char del = ',';

            if (delimiter != null && delimiter.Trim().Length > 0)
            {
                del = Char.Parse(delimiter.Trim());
            }

            if (value != null && value.Trim().Length > 1 && containsHeader != null && containsHeader.Trim().Length > 0)
            {
                TableCSVGrid _wizardWindow = new TableCSVGrid();
                _wizardWindow.csvFileContent = value;
                _wizardWindow.containsHeader = containsHeader == "Yes" ? true : false;
                _wizardWindow.delimiter = del;
                _wizardWindow.Title = title;
                _wizardWindow.ShowDialog();
                _returnValue = _wizardWindow.newContent;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }

        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

    }

    public partial class ListAddonCommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        
        private string _returnValue = string.Empty;

        public ListAddonCommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Window Title", UBotType.String));
        }

        public string FunctionName
        {
            get { return "$edit list csv"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            //string contentName = parameters["csvcontent"];

            string value = parameters["csvcontent"];// ubotStudio.GetVariable(contentName);

            if (value != null && value.Trim().Length > 0)
            {
                ListCSVGrid _wizardWindow = new ListCSVGrid();
                _wizardWindow.csvFileContent = value;
                _wizardWindow.Title = parameters["Window Title"];
                _wizardWindow.ShowDialog();
                _returnValue = _wizardWindow.newContent;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    public partial class ListNoUIAddonCommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        private string _returnValue = string.Empty;

        public ListNoUIAddonCommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
             var ActionInsertReplace = new UBotParameterDefinition("Action Type?", UBotType.String);
            ActionInsertReplace.Options = new[] {"Insert", "Replace" };
            _parameters.Add(ActionInsertReplace);
            _parameters.Add(new UBotParameterDefinition("Position Index", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Text", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Message", UBotType.UBotVariable));
        }

        public string FunctionName
        {
            get { return "$edit list"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            //string contentName = parameters["csvcontent"];

            string content  = parameters["csvcontent"]; //ubotStudio.GetVariable(contentName);
            string action   = parameters["Action Type?"];
            string index    = parameters["Position Index"];
            string text     = parameters["Text"];
            int idx         = -1;
            string errorMessageId = parameters["Message"];

            bool  isIndexValid = int.TryParse(index, out idx);
            if (!isIndexValid)
            {
                ubotStudio.SetVariable(errorMessageId, "Position Index can only have numeric value");
                _returnValue = string.Empty;
            }
            else 
            {
                if (content != null && content.Trim().Length > 0)
                {
                    String[] items = content.Split(new char[] { '\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (idx < 0 || idx > (items.Length - 1))
                    {
                        ubotStudio.SetVariable(errorMessageId, String.Format("Invalid Position Index specified. Allowed values [0 to {0}]", (items.Length - 1)));
                        _returnValue = string.Empty;
                    }
                    else
                    {
                        List<string> lst = new List<string>(items);
                        if (action.Equals("Insert"))
                        {
                            lst.Insert(idx, text);
                        }
                        else if (action.Equals("Replace"))
                        {
                            lst[idx] = text;
                        }
                        else 
                        {
                            ubotStudio.SetVariable(errorMessageId, "Invalid Action Type selected.");
                            _returnValue = string.Empty;
                            return;
                        }
                        _returnValue = String.Join(Environment.NewLine, lst.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray());
                        ubotStudio.SetVariable(errorMessageId, "Success");
                    }
                }
                else 
                {
                    ubotStudio.SetVariable(errorMessageId, "Array is empty");
                    _returnValue = string.Empty;
                }
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    public partial class FindAndReplaceOnListNoUIFunction : IUBotFunction
    {

        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        private string _returnValue = string.Empty;

        public FindAndReplaceOnListNoUIFunction()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("find", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("replace", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Message", UBotType.UBotVariable));
        }

        public string FunctionName
        {
            get { return "$FindAndReplaceInList"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string content = parameters["csvcontent"];
            string findText = parameters["find"].Trim().ToLower();
            string replaceText = parameters["replace"].Trim();
            string errorMessageId = parameters["Message"];

            if (findText.Trim().Length == 0)
            {
                ubotStudio.SetVariable(errorMessageId, "find/search text can't be empty.");
                _returnValue = string.Empty;
            }
            else if (content.Trim().Length == 0)
            {
                ubotStudio.SetVariable(errorMessageId, "content can't be empty.");
            }
            else
            {
                String[] items = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = Regex.Replace(items[i], findText, replaceText.Trim(), RegexOptions.IgnoreCase);
                }
                List<string> retList = items.Where(item => !string.IsNullOrWhiteSpace(item)).Select(item => item).ToList();
                _returnValue = string.Join(Environment.NewLine, retList);
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    public partial class InsertTableRowNoUIFunction : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public InsertTableRowNoUIFunction()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Position", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Data", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("ErrorMessage", UBotType.UBotVariable));
        }

        public string FunctionName
        {
            get { return "$AddNewRowToTableNoUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string tableValue = parameters["csvcontent"];
            string position = parameters["Position"];
            string data = parameters["Data"];
            string msgId = parameters["ErrorMessage"];
            string delimiter = parameters["Delimiter"];

            char del = ',';
            if (delimiter.Trim().Length > 0)
            {
                del = char.Parse(delimiter.Trim());
            }

            int pos = -1;
            bool parsed = int.TryParse(position, out pos);
            if (!parsed)
            {
                ubotStudio.SetVariable(msgId, "Only numeric value allowed for position.");
                _returnValue = string.Empty;
            }
            if (data.IndexOf(del) == -1)
            {
                ubotStudio.SetVariable(msgId, string.Format("Delimiter '{0}' not found on input data.", del));
                _returnValue = string.Empty;
                return;
            }
            if (tableValue != null && tableValue.Trim().Length > 1)
            {
                List<string> rows = tableValue.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (pos < 0 || pos > (rows.Count - 1))
                {
                    ubotStudio.SetVariable(msgId, string.Format("Position can be from {0} to {1}", 0, rows.Count - 1));
                    _returnValue = string.Empty;
                }
                else
                {
                    rows.Insert(pos, data);
                }
                _returnValue = string.Join(Environment.NewLine, rows.ToArray());
                ubotStudio.SetVariable(msgId, "Success");
            }
            else
            {
                ubotStudio.SetVariable(msgId, "Table is empty.");
                _returnValue = string.Empty;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

    }

    public partial class ReplaceInRowNoUIFunction : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public ReplaceInRowNoUIFunction()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Row Position", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Find", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Replace", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("ErrorMessage", UBotType.UBotVariable));
        }

        public string FunctionName
        {
            get { return "$ReplaceInRowNoUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string tableValue = parameters["csvcontent"];
            string delimiter = parameters["Delimiter"];
            string position = parameters["Row Position"];
            string findText = parameters["Find"];
            string replaceText = parameters["Replace"];
            string msgId = parameters["ErrorMessage"];
            int pos = -1;
            char del = ',';
            if (delimiter.Trim().Length > 0)
            {
                del = char.Parse(delimiter.Trim());
            }

            bool parsed = int.TryParse(position, out pos);
            if (!parsed)
            {
                ubotStudio.SetVariable(msgId, "Only numeric value allowed for position.");
                _returnValue = string.Empty;
            }

            if (tableValue != null && tableValue.Trim().Length > 1)
            {
                List<string> rows = tableValue.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (pos < 0 || pos > (rows.Count - 1))
                {
                    ubotStudio.SetVariable(msgId, string.Format("Position can be from {0} to {1}.", 0, rows.Count - 1));
                    _returnValue = string.Empty;
                }
                else
                {
                    if (findText.Trim().Length == 0)
                    {
                        ubotStudio.SetVariable(msgId, "Find text can't be empty.");
                        _returnValue = string.Empty;
                    }
                    else
                    {
                        string[] items = rows[pos].Split(new char[] { del });
                        for (int i = 0; i < items.Length; i++)
                        {
                            items[i] = Regex.Replace(items[i], findText.Trim(), replaceText.Trim(), RegexOptions.IgnoreCase);
                        }
                        rows[pos] = String.Join(del.ToString(), items.ToArray());
                        _returnValue = string.Join(Environment.NewLine, rows.ToArray());
                        ubotStudio.SetVariable(msgId, "Success");
                    }
                }
            }
            else
            {
                ubotStudio.SetVariable(msgId, "Table is empty.");
                _returnValue = string.Empty;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

    }

    public partial class ReplaceInColumnNoUIFunction : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public ReplaceInColumnNoUIFunction()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Col Position", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Find", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Replace", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("ErrorMessage", UBotType.UBotVariable));
        }

        public string FunctionName
        {
            get { return "$ReplaceInColumnNoUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string tableValue = parameters["csvcontent"];
            string delimiter = parameters["Delimiter"];
            string position = parameters["Col Position"];
            string findText = parameters["Find"];
            string replaceText = parameters["Replace"];
            string msgId = parameters["ErrorMessage"];
            int pos = -1;
            char del = ',';
            if (delimiter.Trim().Length > 0)
            {
                del = char.Parse(delimiter.Trim());
            }

            bool parsed = int.TryParse(position, out pos);
            if (!parsed)
            {
                ubotStudio.SetVariable(msgId, "Only numeric value allowed for position.");
                _returnValue = string.Empty;
            }

            if (tableValue != null && tableValue.Trim().Length > 1)
            {
                List<string> rows = tableValue.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < rows.Count; i++)
                {
                    string[] items = rows[i].Split(new char[] { del });
                    if (i == 0)
                    {
                        if (pos < 0 || (pos > items.Length - 1))
                        {
                            ubotStudio.SetVariable(msgId, string.Format("Position can be from {0} to {1}.", 0, items.Length - 1));
                            _returnValue = string.Empty;
                            break;
                        }
                    }
                    if (pos < items.Length)
                    {
                        items[pos] = Regex.Replace(items[pos], findText.Trim(), replaceText.Trim(), RegexOptions.IgnoreCase);
                    }
                    rows[i] = String.Join(del.ToString(), items.ToArray());
                }
                _returnValue = string.Join(Environment.NewLine, rows.ToArray());
                ubotStudio.SetVariable(msgId, "Success");
            }
            else
            {
                ubotStudio.SetVariable(msgId, "Table is empty.");
                _returnValue = string.Empty;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

    }

    public partial class TableAddRowDataCommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public TableAddRowDataCommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            var HeaderYesNo = new UBotParameterDefinition("Contains Headers?", UBotType.String);
            HeaderYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(HeaderYesNo);
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Window Title", UBotType.String));
            var ReadOnlyYesNo = new UBotParameterDefinition("ReadOnly?", UBotType.String);
            ReadOnlyYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(ReadOnlyYesNo);
        }

        public string FunctionName
        {
            get { return "$AddNewRowToTableWithUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string value = parameters["csvcontent"];

            string containsHeader = parameters["Contains Headers?"];

            string readOnly = parameters["ReadOnly?"];

            string delimiter = parameters["Delimiter"];

            string title = parameters["Window Title"];

            char del = ',';

            if (delimiter != null && delimiter.Trim().Length > 0)
            {
                del = Char.Parse(delimiter.Trim());
            }

            if (value != null && value.Trim().Length > 1 && containsHeader != null && containsHeader.Trim().Length > 0)
            {
                TableRowInsert _wizardWindow = new TableRowInsert();
                _wizardWindow.csvFileContent = value;
                _wizardWindow.containsHeader = containsHeader == "Yes" ? true : false;
                if (readOnly.Equals("Yes"))
                {
                    _wizardWindow.MakeReadOnly(true);
                }
                else
                {
                    _wizardWindow.MakeReadOnly(false);
                }
                _wizardWindow.delimiter = del;
                _wizardWindow.Title = title;
                _wizardWindow.ShowDialog();
                _returnValue = _wizardWindow.newContent;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

    }

    public partial class FindAndReplaceInColumnUICommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public FindAndReplaceInColumnUICommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            var HeaderYesNo = new UBotParameterDefinition("Contains Headers?", UBotType.String);
            HeaderYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(HeaderYesNo);
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Window Title", UBotType.String));
            var ReadOnlyYesNo = new UBotParameterDefinition("ReadOnly?", UBotType.String);
            ReadOnlyYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(ReadOnlyYesNo);

        }

        public string FunctionName
        {
            get { return "$FindAndReplaceInColumnUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            //string contentName = parameters["csvcontent"];

            string value = parameters["csvcontent"];

            string containsHeader = parameters["Contains Headers?"];

            string readOnly = parameters["ReadOnly?"];

            string delimiter = parameters["Delimiter"];

            string title = parameters["Window Title"];

            char del = ',';

            if (delimiter != null && delimiter.Trim().Length > 0)
            {
                del = Char.Parse(delimiter.Trim());
            }

            if (value != null && value.Trim().Length > 1 && containsHeader != null && containsHeader.Trim().Length > 0)
            {
                FindAndReplaceColumn _wizardWindow = new FindAndReplaceColumn();
                _wizardWindow.csvFileContent = value;
                _wizardWindow.containsHeader = containsHeader == "Yes" ? true : false;
                _wizardWindow.delimiter = del;
                _wizardWindow.Title = title;

                if (readOnly.Equals("Yes"))
                {
                    _wizardWindow.MakeReadOnly(true);
                }
                else
                {
                    _wizardWindow.MakeReadOnly(false);
                }

                _wizardWindow.ShowDialog();
                _returnValue = _wizardWindow.newContent;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    public partial class AddNewColumnUICommand : IUBotFunction
    {
        // List to hold the parameters we define for our command.
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        private string _returnValue = string.Empty;

        public AddNewColumnUICommand()
        {
            _parameters.Add(new UBotParameterDefinition("csvcontent", UBotType.String));
            var HeaderYesNo = new UBotParameterDefinition("Contains Headers?", UBotType.String);
            HeaderYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(HeaderYesNo);
            _parameters.Add(new UBotParameterDefinition("Delimiter", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Window Title", UBotType.String));
            var ReadOnlyYesNo = new UBotParameterDefinition("ReadOnly?", UBotType.String);
            ReadOnlyYesNo.Options = new[] { "Yes", "No" };
            _parameters.Add(ReadOnlyYesNo);

        }

        public string FunctionName
        {
            get { return "$AddNewColumnUI"; }
        }

        public string Category
        {
            // This is what category our command is categorized as. 
            // If you choose something not already in the toolbox list, a new category will be created.
            get { return "Tables and Lists"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            //string contentName = parameters["csvcontent"];

            string value = parameters["csvcontent"];

            string containsHeader = parameters["Contains Headers?"];

            string readOnly = parameters["ReadOnly?"];

            string delimiter = parameters["Delimiter"];

            string title = parameters["Window Title"];

            char del = ',';

            if (delimiter != null && delimiter.Trim().Length > 0)
            {
                del = Char.Parse(delimiter.Trim());
            }

            if (value != null && value.Trim().Length > 1 && containsHeader != null && containsHeader.Trim().Length > 0)
            {
                AddNewColumn _wizardWindow = new AddNewColumn();
                _wizardWindow.csvFileContent = value;
                _wizardWindow.containsHeader = containsHeader == "Yes" ? true : false;
                _wizardWindow.delimiter = del;
                _wizardWindow.Title = title;

                if (readOnly.Equals("Yes"))
                {
                    _wizardWindow.MakeReadOnly(true);
                }
                else
                {
                    _wizardWindow.MakeReadOnly(false);
                }

                _wizardWindow.ShowDialog();
                _returnValue = _wizardWindow.newContent;
            }
        }

        public bool IsContainer
        {
            // This command does not have any nested commands inside of it, so it is not a container command.
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            // We reference our parameter list we defined above here.
            get { return _parameters; }
        }


        public object ReturnValue
        {
            get { return _returnValue; }
        }

        public UBotType ReturnValueType
        {
            get { return UBotType.String; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }
}
