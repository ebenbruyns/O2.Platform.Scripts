// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.WinFormUI.Utils;
using FluentSharp.WinForms.Controls;
using O2.Core.FileViewers.XSD;
using System.Collections.Generic;
using System.Data;
//O2File:struts-config.cs
//O2File:validation.cs

namespace O2.Core.FileViewers.Struts_1_5
{
    public class MapStrutsForms
    {
    	
        public static IO2Log log = PublicDI.log;



        public static void processStrutsFiles(string testStrutsConfigXmlFile, string testValidationXmlFile)
        {		
            var strutsConfig  = (strutsconfig)Serialize.getDeSerializedObjectFromXmlFile(testStrutsConfigXmlFile, typeof (strutsconfig));
            var validation = (formvalidation)Serialize.getDeSerializedObjectFromXmlFile(testValidationXmlFile, typeof (formvalidation));
			
            //listFormBeans(strutsConfig);
            var mappedFormFields = calculateMappedFormFields(strutsConfig,validation);	
						
            showMappedFormFields(mappedFormFields);
            showInGuiMappedFormFields(mappedFormFields);
            log.info("all done....");
        }
		
        public static void showInGuiMappedFormFields(List<mappedFormField>  mappedFormFields)
        {
            var tableList = (ctrl_TableList)O2AscxGUI.openAscx(typeof (ctrl_TableList), O2DockState.Float, "Table List");
            var dataTable = new DataTable();		
            dataTable.Columns.Add("beanName");
            dataTable.Columns.Add("bean form field");
            dataTable.Columns.Add("validated");		
            dataTable.Columns.Add("actionPaths");		
			
            foreach(var mappedData in mappedFormFields)
                dataTable.Rows.Add(new [] {mappedData.beanName, mappedData.formBeanName.Replace(mappedData.beanName + "_", ""),mappedData.hasValidation.ToString() , mappedData.actionPaths});
            tableList.setDataTable(dataTable);
        }				
		
        public static void showMappedFormFields(List<mappedFormField> mappedFormFields)
        {
            log.info("There are {0} Mapped Form Fields", mappedFormFields.Count);
            foreach(var mappedFormField in mappedFormFields)
                log.info("    {0}  ...   {1}   ...  {2}  ", mappedFormField.formBeanName , mappedFormField.hasValidation, mappedFormField.actionPaths);
        }
		
        public static List<mappedFormField> calculateMappedFormFields(strutsconfig strutsConfig, formvalidation validation)
        {					
            // resolve formsInStructConfic
            /*           var formsInStructConfig = new Dictionary<string, strutsconfigFormbean>();			
			
                      foreach(var formBean in strutsConfig.formbeans)
                          if (formBean.formproperty != null)
                              foreach(var formProperty in formBean.formproperty)
                                  formsInStructConfig.Add(formBean.name + "_" + formProperty.name, formBean);
                      log.info("there are {0} forms in strutsConfig", formsInStructConfig.Count);
                      // resolve in formsInValidation
                     var formsInValidation = new Dictionary<string, formvalidationForm>();
                      foreach(var formBean in validation.formset)
                      {
                          if (formBean.field != null)
                              foreach(var field in formBean.field)
                              {
                                  var beanKey = formBean.name + "_" + field.property ;
                                  if (formsInValidation.ContainsKey(beanKey))
                                      log.error("Duplicate breanKey: {0}: ", beanKey);
                                  else
                                      formsInValidation.Add(beanKey, formBean);
                              }
                      }
              
            log.info("there are {0} forms in validator", formsInValidation.Count);
			
            // resolve formsInActions
            var formsInActions = new Dictionary<string, string>();
            foreach(var action in strutsConfig.actionmappings)
                if (action.name!=null && action.name!= "")
                    if (formsInActions.ContainsKey(action.name) == true)
                        formsInActions[action.name] += " , " + action.path;
                    else
                        formsInActions.Add(action.name,action.path);				
						
            log.info("there are {0} forms mapped to actionMappings", formsInActions.Count);			// map form fields
			
            // calculate mappedFormFields 
            var mappedFormFields = new List<mappedFormField>();
            foreach(var formBeanKey in formsInStructConfig.Keys)
            {						
                var formBeam = formsInStructConfig[formBeanKey];
                //log.info(formBeanKey);
                var mappedFormField = new mappedFormField();
                mappedFormField.beanName = formBeam.name;
                mappedFormField.formBeanName = formBeanKey; //formBean.name;
                mappedFormField.hasValidation = formsInValidation.ContainsKey(formBeanKey); //formBean.name);
                mappedFormField.actionPaths = (formsInActions.ContainsKey(formBeam.name) == true) ? formsInActions[formBeam.name] : "";
                mappedFormFields.Add(mappedFormField);
            }
            return mappedFormFields;
             */
            return null;
        }
		
        public static void listFormBeans(strutsconfig strutsConfig)
        {			
            foreach(var formBean in strutsConfig.formbeans)
            {
                //var formBean = strutsConfig[formFieldKey];
                log.info("name:{0}      -      type:{1}",formBean.name, formBean.type);
                if (formBean.formproperty!= null)
                    foreach(var formProperty in formBean.formproperty)
                        log.info("     {0} : {1}", formProperty.name, formProperty.type);
            }
        }
    }

    public class mappedFormField
    {
        public string beanName {get;set;} 
        public string formBeanName {get;set;} 
        public bool hasValidation  {get;set;} 
        public string actionPaths  {get;set;} 
    }
}
