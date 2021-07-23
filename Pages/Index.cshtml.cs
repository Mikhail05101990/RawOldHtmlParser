using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Data;
using System.Text.RegularExpressions;

namespace webAnaliser.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        string str;
        string[] partsArr;

        string[] strs = new string[0];

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            str = await GetLocalFileText(@"/media/joker/hdd/webcore31/webAnaliser/view-source_https___tinder.com.html");
            //str = await GetResponseText();
            partsArr = GetWordArray(str);
            strs = ReturnStrigs(partsArr,200,true);
            ViewData["NiceStrings"] = strs;
            ViewData["DOM"]="https://tinder.com";
        }

        public async Task<string> GetLocalFileText(string path){
            try	
            {
                using (var sr = new StreamReader(path))
                {
                    return str = await sr.ReadToEndAsync();
                };
            }
            catch(HttpRequestException e)
            {   
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public async Task<string> GetResponseText()
        {
            try	
            {
                string responseBody = await client.GetStringAsync("https://tinder.com");
                return responseBody;
            }
            catch(HttpRequestException e)
            {   
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public static bool CheckIfScript(string link){
            string[] arr = link.Split(".");
            if(arr.Length>0){
                if(arr[arr.Length-1]=="js"){
                    return true;
                }
            }
            return false;
        }

        public string[] GetWordArray(string text){
            string[] arr = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return arr;
        }

        public string[] GetHead(string[] arr){
            int tagIndStart=-1;
            int tagIndEnd=-1;
            bool lookForEnd=false;
            bool loolForTagNameOnly=false;

            for (int i =0; i<arr.Count(); i++)
            {
                if(lookForEnd){
                    if(arr[i].Contains("</head>")){
                        tagIndEnd=i;
                    }

                }else{
                    if(arr[i] == "<"){
                        loolForTagNameOnly=true;
                    }
                    if(loolForTagNameOnly){
                        if(arr[i].ToLower() =="head"){
                            tagIndStart=i;
                            lookForEnd=true;
                        }
                    }else{
                        if(str.Contains("<head")){
                                tagIndStart=i;
                                lookForEnd=true;
                        }
                    }
                }
            }
            string[] res = new string[0];

            if(tagIndEnd>0){
                res = new string[tagIndEnd-tagIndStart];
                Array.Copy(arr, tagIndStart, res, 0, tagIndEnd-tagIndStart);
            }

            return res;
        }

        public static string GetParName(int i){
            return "par"+(i+1);
        }

        public static string GetFullLink(string domen, string part){
            return domen+part;
        }

        public void AppendTagTabs(ref string str, string tabs, ref int row_counter){
            row_counter++;
            str += row_counter;
            str +=tabs;
        }

        public void AddOneTab(ref string tabs){
            tabs+=@"\t";
        }
        
        public void RemoveOneTab(ref string tabs){
            if(tabs.Length>0){
                tabs=tabs.Substring(0,tabs.Length-2);
            }
            
        }

        public void CleanTag(ref string tag, ref bool bracketstart, ref bool tagstart, ref bool startclosed, ref List<string> taglist, ref bool tagEndOpened, ref bool tagEndClosed, int rcount,ref int row_counter, ref bool stopRunning){
            tagstart=false;
            bracketstart=false;
            startclosed=false;
            tagEndOpened=false;
            tagEndClosed=false;
            if(tag!=""){
                if (rcount>row_counter){
                    taglist.Add(tag);
                }else{
                    if(rcount==row_counter){
                        taglist.Add(tag);
                    }
                    stopRunning=true;
                }
                
                tag="";
            }
            
        }

        public void AppendStringPart(ref string str, string part){
            if(part!=""){
                str += " ";
                str += part; 
            }
        }

        public bool BracketInMiddle(string str){
            string val;
            if(str.Length>2){
                val = str.Substring(1,str.Length-2);
            }else{
                val=str;
            }
            if (val.Contains("<") || val.Contains(">")){
                return true;
            }
            return false;
        }
        
        public int GetTagType(string item){
            if(item[0]=='<'){
                if(item[item.Length-1]=='>'){
                    return 0;
                }else{
                    return 1;
                }
            }else{
                if(item[item.Length-1]=='>'){
                    return 2;
                }
            }
            
            return -1;
        }

        public int[] GetBracketType(string item){

            int[] result = new int[2]{-1,-1};
            if(item.Length>2){
                string middle= item.Substring(1,item.Length-2);
                int Left = middle.IndexOf("<");
                int Right = middle.IndexOf(">");
                int Batter = middle.IndexOf("><");
                int Dend = middle.IndexOf("</");
                int SCend = middle.IndexOf("/>");
                string txt = Regex.Match(item,@"\<([^\>]*)\>").Groups[1].Value;
                int group = item.IndexOf("<" + txt + ">");
                
                result[0] = Left;
                result[1]=3;
                
                if(Right>0){
                    if(result[0]>-1){
                        if(Right<result[0]){
                            result[0]=Right;
                            result[1]=4;
                        }
                    }else{
                        result[0]=Right;
                        result[1]=4;
                    }
                }

                if(SCend>0){
                    if(result[0]>-1){
                        if(SCend<result[0]){
                            result[0]=SCend;
                            result[1]=6;
                        }
                    }else{
                        result[0]=SCend;
                        result[1]=6;
                    }
                }

                if(Batter>-1){
                    if(result[0]>-1){
                        if(Batter<=result[0]){
                            result[0]=Batter;
                            result[1]=1;
                        }
                    }else{
                        result[0]=Batter;
                        result[1]=1;         
                    }
                }
                if(Dend>-1){
                    if(result[0]>-1){
                        if(Dend<=result[0]){
                            result[0]=Dend;
                            result[1]=2;
                        }
                    }else{
                        result[0]=Dend;
                        result[1]=2;
                    }
                    
                }
                if(group>-1){
                    if(result[0]>-1){
                        if(group<=result[0]){
                            result[0]=group;
                            result[1]=5;
                        }else{
                            if((group-Dend)==1){
                                result[0]=group;
                                result[1]=5;
                            }
                        }
                    }else{
                        result[0]=group;
                        result[1]=5;
                    }
                }
                
            }
            return result;
            
        }

        public bool CheckIfNotSingle(string[] arr, List<string> singles, string val){
            foreach(var item in singles){
                if(item==val){
                    return false;
                }
            }
            string str = "/" + val;
            foreach (var item in arr){
                if(item.Contains(str)){
                    return true;
                }
            }
            return false;
        }

        public void AddUniqueValueToList(ref List<string> list, string val){
            foreach(var item in list){
                if(item == val){
                    return;
                }
            }
            list.Add(val);
        }

        public void HandleMiddleBracket(string[] arr, ref string item, ref string tagName, ref bool tagStartClosed, ref string tag, ref bool lookForTagNameOnly, ref string cur_tagName,ref bool startBracketFound, ref bool tagStartFound, ref List<string> HtmlSrings, ref bool enableCount,ref int row_counter, ref int rcount, ref bool stopRunning, ref List<string> SingleTags, ref List<string> OpenedTags, ref string tabs, ref bool tagEndOpened, ref bool tagEndClosed, ref bool finishedTag){
            int[] vls= GetBracketType(item);
            int bracketPos = vls[0];
            int bracketType = vls[1];
            if(bracketPos>-1){
                //handle batter
                if(bracketType==1){
                    string ending = item.Substring(0,bracketPos+2);
                    tagStartClosed=true;
                    AppendStringPart(ref tag, ending); 
                    string startNew = item.Substring(bracketPos+2);
                    if(startNew=="<"){
                        lookForTagNameOnly=true;
                    }else{
                        if(startNew.Contains("</")){
                        //this is an end
                            if(cur_tagName==startNew.Substring(2,startNew.Length-3)){
                                AppendStringPart(ref tag,item);
                                //prepare to look for a new tag
                                CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                            }else{
                                //other start
                                //it is a singe tag
                                if(!enableCount){
                                    HtmlSrings.Add(tag);
                                }else{
                                    if(row_counter == rcount){
                                        stopRunning=true;
                                    }
                                    HtmlSrings.Add(tag);
                                }
                                AddUniqueValueToList(ref SingleTags, tagName);
                                //work with current end
                                if(OpenedTags.Count>0){
                                    OpenedTags.RemoveAt(OpenedTags.Count-1);
                                }
                                CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                RemoveOneTab(ref tabs);
                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                AppendStringPart(ref tag,item);
                                CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                            }
                        }else{
                            //it is a beginning
                            bool notSingle = false;
                            notSingle = CheckIfNotSingle(arr, SingleTags, tagName);
                            if(!notSingle){
                                AddUniqueValueToList(ref SingleTags,tagName);
                                CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                            }else{
                                OpenedTags.Add(tagName);
                                AddOneTab(ref tabs);
                            }
                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                            tagName = startNew.Substring(1);
                            AppendTagTabs(ref tag, tabs, ref row_counter);
                            AppendStringPart(ref tag,startNew);
                            tagStartFound=true;
                            startBracketFound=true;
                            if(startNew.Contains('>')){
                                tagStartClosed=true;
                            }else{
                                item="";
                            }
                        
                        }
                    }
                }
                if(bracketType==4){
                    AppendStringPart(ref tag, item);
                    tagStartClosed=true;
                    item="";
                }
                if(bracketType==6){
                    AppendStringPart(ref tag, item.Substring(0,bracketPos+3));
                    AddUniqueValueToList(ref SingleTags, tagName);
                    CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                    item=item.Substring(bracketPos+3);
                    vls= GetBracketType(item);
                    if(item==""){
                        finishedTag=true;
                    }
                }
                if(bracketType==5){
                    do{
                        //enshure that there is not tags
                        if(item.Contains("<") | item.Contains(">")){
                          //  if(tagStartClosed){
                                //check if it is an end
                                cur_tagName= Regex.Match(item,@"\<([^\>]*)\>").Groups[1].Value;
                                if(cur_tagName.Contains("/")){
                                    string name= cur_tagName.Substring(1);
                                    if(name==tagName){
                                        string appPart=item.Substring(0,bracketPos + cur_tagName.Length+2);
                                        item=item.Substring(bracketPos + cur_tagName.Length+2);
                                        if(!item.Contains("<") && !item.Contains(">")){
                                            AppendStringPart(ref tag, appPart+item);
                                            item="";
                                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                            //finishedTag=true;
                                        }else{
                                            AppendStringPart(ref tag, appPart);
                                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                            //itemVal=itemVal.Substring(bracketPos + cur_tagName.Length+2);
                                            //finishedTag=true;
                                        }
                                    }else{
                                        //it is over end
                                        RemoveOneTab(ref tabs);
                                        AppendTagTabs(ref tag, tabs, ref row_counter);
                                        string part=item.Substring(0,bracketPos + cur_tagName.Length+2);
                                        string rest =item.Substring(bracketPos + cur_tagName.Length+2);
                                        if(rest.Contains("<") | rest.Contains(">")){
                                            AppendStringPart(ref tag, part);
                                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                            item = rest;
                                        }else{
                                            AppendStringPart(ref tag, item);
                                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                            item="";
                                        }
                                        if(OpenedTags.Count>0){
                                            OpenedTags.RemoveAt(OpenedTags.Count-1);
                                        } 
                                    }
                                }else{
                                    //this is a new tag
                                    AddOneTab(ref tabs);
                                    bool check = CheckIfNotSingle(arr, SingleTags, tagName);
                                    if(!check){
                                        AddUniqueValueToList(ref SingleTags,tagName);
                                    }
                                    if(check && tagStartClosed && !tagEndClosed){
                                        OpenedTags.Add(tagName);
                                    }
                                    CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                                    tagName = cur_tagName;
                                    AppendTagTabs(ref tag, tabs, ref row_counter);
                                    AppendStringPart(ref tag, "<" + tagName + ">");
                                    tagStartFound=true;
                                    startBracketFound=true;
                                    tagStartClosed=true;
                                    item=item.Substring(tagName.Length+2);
                                }
                           // }
                        }else{
                            //add tag start attribute
                            AppendStringPart(ref tag, tagName); 
                        }
                        vls= GetBracketType(item);
                        bracketPos = vls[0];
                        bracketType = vls[1];
                    }while (bracketType==5);
                }
                if(item!=""){
                        int tagType = GetTagType(item);
                        if(tagType==1){
                            if(finishedTag){
                                tagName=item.Substring(1);
                            }
                            bool check = CheckIfNotSingle(arr,SingleTags,tagName);
                            if(check && tagStartClosed && !tagEndClosed){
                                OpenedTags.Add(tagName);
                            }
                            cur_tagName = item.Substring(1);
                            if(!finishedTag && tagStartClosed){
                                AddOneTab(ref tabs);
                            }
                            CleanTag(ref tag, ref startBracketFound, ref tagStartFound, ref tagStartClosed, ref HtmlSrings, ref tagEndOpened, ref tagEndClosed, rcount, ref row_counter, ref stopRunning);
                            tagName=cur_tagName;
                            AppendTagTabs(ref tag, tabs, ref row_counter);
                            AppendStringPart(ref tag, "<" + cur_tagName);
                            tagStartFound=true;
                            startBracketFound=true;
                            tagStartClosed=false;
                            finishedTag=false;
                            item="";
                        }
                    }
                
            }else{
                //error when check middle bracket
                //append as it is
                tagName=item.Substring(0,item.Length-1);
                tagStartClosed=true;
            }
        }

        public string[] ReturnStrigs(string[] arr, int rcount, bool enableCount){
            bool lookForTagNameOnly=false;
            bool stopRunning = false;
            int row_counter=0;
            int[] brarr = new int[2]{-1,-1}; 
            string tag="";
            string tagName="";
            string cur_tagName="";
            string tabs="";
            bool LeftFound=false;
            bool finishedTag=false;
            bool startFound=false;
            bool startClosed = false;
            bool EndFound = false;
            bool EndClosed = false;
            int tagType=-1;
            string itemVal="";
            bool middleBracket = false;
            List<string> OpenedTags=new List<string>();
            List<string> HtmlSrings = new List<string>();
            List<string> SingleTags = new List<string>();

            foreach (var item in arr)
            {
                itemVal=item;
                if(!stopRunning){
                    if(tag.Contains("129")){

                    }
                    if(item.Contains("/head")){

                    }
                    //permission to work is valid
                    if(!startFound){
                        //start bracket not found
                        if(!LeftFound){
                            middleBracket=BracketInMiddle(itemVal);
                            if(!middleBracket){
                                tagType=GetTagType(itemVal);
                                if(tagType==0){
                                    AppendTagTabs(ref tag, tabs, ref row_counter);
                                    AppendStringPart(ref tag, itemVal);
                                    tagName=itemVal.Substring(1,itemVal.Length-2);
                                    startFound=true;
                                    startClosed=true;
                                    LeftFound=false;
                                    continue;
                                }else{
                                    if(tagType==1){
                                        AppendTagTabs(ref tag, tabs, ref row_counter);
                                        AppendStringPart(ref tag, itemVal);
                                        startFound=true;
                                        tagName=itemVal.Substring(1);
                                        continue;
                                    }
                                }

                            }else{
                                //bracket in middle
                                HandleMiddleBracket(arr, ref itemVal, ref tagName, ref startClosed, ref tag, ref lookForTagNameOnly, ref cur_tagName, ref LeftFound, ref startFound, ref HtmlSrings, ref enableCount, ref row_counter, ref rcount, ref stopRunning, ref SingleTags, ref OpenedTags, ref tabs, ref EndFound, ref EndClosed, ref finishedTag);
                            }

                            brarr = GetBracketType(itemVal);
                            string tail="";
                            if(brarr[1]>0){
                                if(brarr[1]==5){
                                    string txt = Regex.Match(itemVal, @"\<([^>]*)\>").Groups[1].Value;
                                    AppendTagTabs(ref tag, tabs, ref row_counter);
                                    if(brarr[0]==0){
                                        AppendStringPart(ref tag, "<" + txt + ">");
                                        tail = itemVal.Substring(txt.Length+2);
                                    }else{
                                        string str = itemVal.Substring(0,brarr[0]+txt.Length+2);
                                        AppendStringPart(ref tag, str);
                                        tail = itemVal.Substring(brarr[0]+txt.Length+2);
                                    }
                                    startFound=true;  
                                    startClosed=true; 
                                    
                                }
                                if(tail!=""){
                                    //work with tail
                                }
                            }else{
                                //bracket still not found
                                //some text
                                AppendStringPart(ref tag, itemVal);   
                            }
                        }else{
                            //Left bracket found only
                            brarr = GetBracketType(itemVal);
                            if(brarr[1]<0){
                                tagName = itemVal;
                                startFound=true;
                            }
                            if(brarr[1]==4){
                                tagName=itemVal.Substring(0,brarr[0]);
                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                AppendStringPart(ref tag, tagName + ">");
                                startClosed=true;
                            }
                            
                        }
                    }else{
                        //start found
                        //check if start closed
                        if(startClosed){
                            if(itemVal.Contains("<") | itemVal.Contains(">")){
                                middleBracket= BracketInMiddle(itemVal);
                                if(!middleBracket){
                                    tagType = GetTagType(itemVal);
                                    if(tagType==1){
                                        if(itemVal[1]=='/'){
                                            cur_tagName=itemVal.Substring(2);
                                            if(tagName==cur_tagName){
                                                AppendStringPart(ref tag, itemVal);
                                                EndFound=true;
                                            }else{
                                                bool check = CheckIfNotSingle(arr, SingleTags, tagName);
                                                OpenedTags.Add(tagName);
                                                AddUniqueValueToList(ref SingleTags, cur_tagName);
                                                CleanTag(ref tag, ref LeftFound, ref startFound, ref startClosed, ref HtmlSrings, ref EndFound, ref EndClosed, rcount, ref row_counter, ref stopRunning);
                                                tagName=cur_tagName;
                                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                                AppendStringPart(ref tag, tagName + ">");
                                            }
                                        }
                                    }
                                }else{
                                    HandleMiddleBracket(arr, ref itemVal, ref tagName, ref startClosed, ref tag, ref lookForTagNameOnly, ref cur_tagName, ref LeftFound, ref startFound, ref HtmlSrings, ref enableCount, ref row_counter, ref rcount, ref stopRunning, ref SingleTags, ref OpenedTags, ref tabs, ref EndFound, ref EndClosed, ref finishedTag);
                                }

                            }else{
                                //inner text
                                AppendStringPart(ref tag, itemVal);
                                continue;
                            }
                        }else{
                            //look for start end
                            if(itemVal.Contains(">")){
                                //work with tags inside
                                //!!!!!!!!!!!!!!!!!!!
                                if(itemVal[0]=='>'){
                                    AppendStringPart(ref tag, ">");
                                    itemVal=itemVal.Substring(1);
                                    startClosed=true;
                                }else{
                                    if(itemVal.Substring(0,2)=="/>"){
                                        string appStr=itemVal.Substring(2);
                                        if(!appStr.Contains("<") && !appStr.Contains(">")){
                                            AppendStringPart(ref tag, itemVal);
                                            CleanTag(ref tag, ref LeftFound, ref startFound, ref startClosed, ref HtmlSrings, ref EndFound, ref EndClosed, rcount, ref row_counter, ref stopRunning);
                                            continue;
                                        }else{
                                            AppendStringPart(ref tag, "/>");
                                            CleanTag(ref tag, ref LeftFound, ref startFound, ref startClosed, ref HtmlSrings, ref EndFound, ref EndClosed, rcount, ref row_counter, ref stopRunning);
                                            itemVal=item.Substring(2);
                                            tagType = GetTagType(itemVal);
                                            if(tagType==1){
                                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                                AppendStringPart(ref tag, itemVal);
                                                startFound=true;
                                                LeftFound=true;
                                                continue;
                                            }
                                        } 
                                    }
                                }
                                middleBracket= BracketInMiddle(itemVal);
                                if(!middleBracket){
                                    tagType = GetTagType(itemVal);
                                    if(tagType==1){
                                        if(item[1]=='/'){
                                            cur_tagName=item.Substring(2);
                                            if(tagName==cur_tagName){
                                                AppendStringPart(ref tag, itemVal);
                                                EndFound=true;
                                            }else{
                                                bool check = CheckIfNotSingle(arr, SingleTags, tagName);
                                                OpenedTags.Add(tagName);
                                                AddUniqueValueToList(ref SingleTags, cur_tagName);
                                                CleanTag(ref tag, ref LeftFound, ref startFound, ref startClosed, ref HtmlSrings, ref EndFound, ref EndClosed, rcount, ref row_counter, ref stopRunning);
                                                tagName=cur_tagName;
                                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                                AppendStringPart(ref tag, tagName + ">");
                                            }
                                        }
                                    }
                                }else{
                                    HandleMiddleBracket(arr, ref itemVal, ref tagName, ref startClosed, ref tag, ref lookForTagNameOnly, ref cur_tagName, ref LeftFound, ref startFound, ref HtmlSrings, ref enableCount, ref row_counter, ref rcount, ref stopRunning, ref SingleTags, ref OpenedTags, ref tabs, ref EndFound, ref EndClosed, ref finishedTag);
                                }
                                //!!!!!!!!!!!!!!!!!!!!!!

                            }else{
                                //inner attributes
                                AppendStringPart(ref tag, itemVal);
                                continue;
                            }
                        }
                    }
                    if(startClosed){
                        //look for end
                        if(itemVal.Contains("<") | itemVal.Contains(">")){
                            //work tags inside
                            //!!!!!!!!!!!!!!!!!!!
                                if(itemVal[0]=='>'){
                                    AppendStringPart(ref tag, ">");
                                    itemVal=itemVal.Substring(1);
                                }
                                middleBracket= BracketInMiddle(itemVal);
                                if(!middleBracket){
                                    tagType = GetTagType(itemVal);
                                    if(tagType==1){
                                        if(itemVal[1]=='/'){
                                            cur_tagName=itemVal.Substring(2);
                                            if(tagName==cur_tagName){
                                                AppendStringPart(ref tag, itemVal);
                                                EndFound=true;
                                            }else{
                                                bool check = CheckIfNotSingle(arr, SingleTags, tagName);
                                                OpenedTags.Add(tagName);
                                                AddUniqueValueToList(ref SingleTags, cur_tagName);
                                                CleanTag(ref tag, ref LeftFound, ref startFound, ref startClosed, ref HtmlSrings, ref EndFound, ref EndClosed, rcount, ref row_counter, ref stopRunning);
                                                tagName=cur_tagName;
                                                AppendTagTabs(ref tag, tabs, ref row_counter);
                                                AppendStringPart(ref tag, tagName + ">");
                                            }
                                        }
                                    }
                                }else{
                                    HandleMiddleBracket(arr, ref itemVal, ref tagName, ref startClosed, ref tag, ref lookForTagNameOnly, ref cur_tagName, ref LeftFound, ref startFound, ref HtmlSrings, ref enableCount, ref row_counter, ref rcount, ref stopRunning, ref SingleTags, ref OpenedTags, ref tabs, ref EndFound, ref EndClosed, ref finishedTag);
                                }
                                //!!!!!!!!!!!!!!!!!!!!!!
                        }else{
                            //inner text
                            AppendStringPart(ref tag, itemVal);
                        }
                    }
                    
                }


                
            }

            return HtmlSrings.ToArray();
        }





    }
}

