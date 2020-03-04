﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XMLDatabase.Data.Base;

namespace XMLDatabase.Database
{
    public class XMLDatabaseManager
    {
        private XElement database;


        #region CLASS FUNCTIONS

        //  ----------------------------------------------------------------------------------------------------
        /// <summary> XMLDatabaseManager Contructior with loaded XML Database. </summary>
        /// <param name="xmlDatabase"> Loaded XML Database. </param>
        public XMLDatabaseManager(XElement xmlDatabase)
        {
            database = xmlDatabase;
        }

        //  ----------------------------------------------------------------------------------------------------
        /// <summary> Constructor for new empty XML Database. </summary>
        /// <param name="dataModelsNames"> Array of Data Models Classes Names. </param>
        /// <returns> New XML Database. </returns>
        public static XElement CreateXMLDatabase( string[] dataModelsNames = null )
        {
            //  Create empty XML Database Root element.
            var xmlDatabase = new XElement("ROOT");

            //  Fill XML Database with Names of Data Models from list if exsit.
            if (dataModelsNames != null)
            {
                foreach (var dataModelName in dataModelsNames)
                {
                    var newNode = new XElement(dataModelName);
                    xmlDatabase.Add(newNode);
                }
            }

            return xmlDatabase;
        }

        #endregion CLASS FUNCTIONS


        #region DATABASE MANAGEMENT FUNCTIONS

        //  ----------------------------------------------------------------------------------------------------
        public void GetDataModelInstance()
        {
            //
        }

        //  ----------------------------------------------------------------------------------------------------
        public void GetDataModelInstances()
        {
            //
        }

        //  ----------------------------------------------------------------------------------------------------
        /// <summary> Add Model Class Instance into XML Database. </summary>
        /// <param name="dataModel"> Interface of Model Class Instance. </param>
        public void AddDataModelInstance(IModel dataModel)
        {
            //  If database contains Node for specified Model Class - add model into database.
            if (HasDataModelInstanceKey(dataModel.GetName()))
            {
                var targetNode = database.Element(dataModel.GetName());
                targetNode.Add(dataModel.AsXML());
            }
        }

        //  ----------------------------------------------------------------------------------------------------
        public void UpdateDataModelInstance(IModel dataModel)
        {
            if (HasDataModelInstanceKey(dataModel.GetName()))
            {
                //
            }
        }

        //  ----------------------------------------------------------------------------------------------------
        public void RemoveDataModelInstance(IModel dataModel)
        {
            //
        }

        #endregion DATABASE MANAGEMENT FUNCTIONS


        #region DATABASE INTEGRITY FUNCTIONS

        //  ----------------------------------------------------------------------------------------------------
        /// <summary> Check if Root element of XML Database contains List of dataModelInstances. </summary>
        /// <param name="dataModel"> Interface of Data Model Instance. </param>
        /// <returns> True - if contains, False - otherwise. </returns>
        public bool HasDataModelInstanceKey(string dataModelKey)
        {
            return database.Elements().Where(item => item.Name == dataModelKey).Count() > 0;
        }

        //  ----------------------------------------------------------------------------------------------------
        public bool HasDataModelInstance(IModel dataModel)
        {
            //  Get instance of specified Node that contains Model Class Instances.
            var targetNode = database.Element(dataModel.GetName());

            if (targetNode != null)
            {
                //  Check if targetNode contains Model Class Instance where "id" is equal to dataModel "id".
                return targetNode.Elements().Where(
                    item => item.Attribute("id").Value == dataModel.GetIdentifier()).Count() > 0;
            }

            return false;
        }

        #endregion DATABASE INTEGRITY FUNCTIONS

    }
}
