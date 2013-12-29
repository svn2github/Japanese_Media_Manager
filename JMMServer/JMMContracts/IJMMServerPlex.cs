﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;
using JMMContracts.PlexContracts;

namespace JMMContracts
{
    [ServiceContract]
    public interface IJMMServerPlex
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "GetFilters/{UserId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,Method="*")]
        System.IO.Stream GetFilters(string UserId);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMetadata/{UserId}/{TypeId}/{Id}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, Method = "*")]
        System.IO.Stream GetMetadata(string UserId, string TypeId, string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetFile/{Id}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        System.IO.Stream GetFile(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "GetUsers", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        System.IO.Stream GetUsers();

        [OperationContract]
        [WebGet(UriTemplate = "Search/{UserId}/{limit}/{query}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        System.IO.Stream Search(string UserId, string limit, string query);

        [OperationContract]
        [WebGet(UriTemplate = "GetSupportImage/{name}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        System.IO.Stream GetSupportImage(string name);
       


    }
}
