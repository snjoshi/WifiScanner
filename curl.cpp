#include <iostream>
#include <string>
#include <curl/curl.h>


static size_t WriteCallback(void *contents, size_t size, size_t nmemb, void *userp)
{
    ((std::string*)userp)->append((char*)contents, size * nmemb);
    return size * nmemb;
}

int main(void)
{
  CURL *curl;
  CURLcode res;
  std::string readBuffer;
  
  char szJsonData[1024];
	memset(szJsonData, 0, sizeof(szJsonData));
	string strJson = "{";
	strJson +="\"product_key\" : \"yyrtrtgccbfhjdfhjjhfdudfuyyueruieri\",";
	strJson +="\"customer_name\" : \"H Singh\",";
	strJson += "\"customer_number\" : \"7888690909\",";
	strJson += "\"customer_email\" : \"harjinders17@gmail.com\",";
	strJson += "\"dealer_name\":\"R Kumar\",";
	strJson += "\"dealer_number\":\"8899790909\",";
	strJson += "\"time\":\"2021-06-11 15:04:14\"";
	strJson += "}";
	strcpy(szJsonData, strJson.c_str());

  curl = curl_easy_init();
  if(curl) {
    curl_easy_setopt(curl, CURLOPT_URL, "https://www.ttbinternetsecurity.com/erps/api/v1/callback_api");
	curl_slist *plist = curl_slist_append(NULL,
				"Content-Type:application/json;charset=UTF-8");
			curl_easy_setopt(pCurl, CURLOPT_HTTPHEADER, plist);
			curl_easy_setopt(pCurl, CURLOPT_POSTFIELDS, szJsonData);
    
    res = curl_easy_perform(curl);
	// Check for errors
			if (res != CURLE_OK)
			{
				printf("curl_easy_perform() failed:%s\n", curl_easy_strerror(res));
			}
    curl_easy_cleanup(curl);

    std::cout << res << std::endl;
  }
  return 0;
}