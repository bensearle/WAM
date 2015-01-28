#uses "CtrlHTTP"
#uses "CtrlXmlRpc"
#uses "xmlrpcHandlerCommon.ctl"
#uses "CtrlZlib"

int HTTP_PORT = 3870;

main()
{
  //Start the HTTP Server
  if (httpServer(false, HTTP_PORT) < 0)
  {
   DebugN("ERROR: HTTP-server can't start. --- Check license");
   return;
  }

  //Start the XmlRpc Handler
  httpConnect("xmlrpcHandler", "/RPC2");
}

mixed xmlrpcHandler(const mixed content, string user, string ip, dyn_string ds1, dyn_string ds2, int connIdx)
{
  int ret;  
  string sMethod, sRet;
  dyn_mixed daArgs;
  mixed methodResult;
  mixed xmlResult;
  string cookie = httpGetHeader(connIdx, "Cookie");
  dyn_errClass derr;

  DebugN("Content we recieved: " + content);
  
  user="root";
  
  //Decode content
  ret = xmlrpcDecodeRequest(content, sMethod, daArgs);

  derr = getLastError();
  if (ret < 0 || dynlen(derr)>=1)
  {
    throwError(derr);
    
    //Output Error
    err = xmlRpcMakeError(PRIO_SEVERE, ERR_SYSTEM, ERR_PARAMETER, "Error parsing xml-rpc stream", "Method: "+sMethod);
    throwError(derr);

    return xmlrpcReturnFault(derr);
  } 

  //Start own method handler
  methodResult = methodHandlerOwn(sMethod, daArgs, user, cookie);

  derr = xmlRpcGetErrorFromResult(methodResult); /* Get error from result if error occurred */
  if (dynlen(derr) > 0) //Error occurred
  {
    throwError(derr);
    return makeDynString(xmlrpcReturnFault(derr), "Content-Type: text/xml");
  }
  
  sRet = xmlrpcReturnSuccess(methodResult); //Encode result

  //Compress the result if the other side allows it
  if ( strlen(sRet) > 1024 && strpos(httpGetHeader(connIdx, "Accept-Encoding"),"gzip") >= 0)
  {
    //Return compressed content
    blob b;
    gzip(sRet, b);

    xmlResult = makeDynMixed(b,"Content-Type: text/xml","Content-Encoding: gzip");
  }
  else
  {
    //Return plain content
    xmlResult = makeDynString(sRet, "Content-Type: text/xml");
  }
  return xmlResult;
}

mixed methodHandlerOwn(string sMethod, dyn_mixed &asValues, string user, string cookie)
{
  switch (sMethod)
  {
    case "pvss.test.HelloXMLRPCWorld": 
      return "HelloXMLRPCWorld";
    case "woa.poke":
      DebugN("**all " + asValues);
      string s = ""+asValues;
      dyn_string pairs;
      s = strrtrim(s,"}");
      s = strltrim(s,"{");
      pairs=strsplit(s, ",");
      dyn_string keys;
      dyn_string values;
      time timestamp;
      string guid;
      for (int i = 1; i <= dynlen(pairs); i++)
      {
        dyn_string couple;
        couple=strsplit(pairs[i], ":");
        string key = strltrim(strrtrim(couple[1],"\""),"\"");
        string value = strltrim(strrtrim(couple[2],"\""),"\"");
        DebugN("k=" + key + " v=" + value);
        if (key=="Time"){
          timestamp = (time)(float)value;
          DebugN(value); 
          DebugN((time)(float)value);
          //Debug(currentTimeNow());
          DebugN(timestamp); 
          }
        else if (key=="AssetGuid"){
            guid = value;
          } 
        else {
            dynAppend(values, value);
            dynAppend(keys, key);
          }
      }
      for (int i = 1; i <= dynlen(keys); i++)
      {
        keys[i] = guid+"."+keys[i]+":_original.._value";
      }
        dpSetTimed(timestamp,keys,values);
      //DebugN("did db work?= " + dpSetTimed(timestamp,keys,values)+ "  " + getLastError());
      return 98;
    default:
      return methodHandlerCommon(sMethod, asValues, user, cookie);
  }
}

