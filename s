warning: LF will be replaced by CRLF in Course/Content/Themes/Default/bootstrap.css.
The file will have its original line endings in your working directory.
[1mdiff --git a/.vs/Course/v14/.suo b/.vs/Course/v14/.suo[m
[1mindex 2e3f218..c66a262 100644[m
Binary files a/.vs/Course/v14/.suo and b/.vs/Course/v14/.suo differ
[1mdiff --git a/Course/Content/Site/Edit.css b/Course/Content/Site/Edit.css[m
[1mindex c12832b..c84ebf0 100644[m
[1m--- a/Course/Content/Site/Edit.css[m
[1m+++ b/Course/Content/Site/Edit.css[m
[36m@@ -37,7 +37,6 @@[m
 }[m
 [m
 #creative_name_edit > i {[m
[31m-    font-size: 20px;[m
     /*color: white;*/[m
 }[m
 [m
[36m@@ -79,10 +78,10 @@[m
     margin-bottom: 12px;[m
 }[m
 [m
[31m-    #header_name > h3 {[m
[31m-        display: inline-block;[m
[31m-        margin: 0;[m
[31m-    }[m
[32m+[m[32m#header_name > h3 {[m
[32m+[m[32m    display: inline-block;[m
[32m+[m[32m    margin: 0;[m
[32m+[m[32m}[m
 [m
 #header_name_edit {[m
     display: inline-block;[m
[1mdiff --git a/Course/Content/Themes/Default/Site.css b/Course/Content/Themes/Default/Site.css[m
[1mindex 1864dd7..f6d82be 100644[m
[1m--- a/Course/Content/Themes/Default/Site.css[m
[1m+++ b/Course/Content/Themes/Default/Site.css[m
[36m@@ -16,6 +16,22 @@[m
     white-space: normal;[m
 }[m
 [m
[32m+[m[32m.shortLabel{[m
[32m+[m[32m    max-width:250px;[m
[32m+[m[32m    text-overflow:ellipsis;[m
[32m+[m[32m    display:block;[m
[32m+[m[32m    overflow:hidden;[m
[32m+[m[32m    white-space:nowrap;[m
[32m+[m[32m}[m
[32m+[m
[32m+[m[32m@media (max-width: 700px) {[m
[32m+[m[32m  #page_header {[m
[32m+[m[32m    display: none;[m
[32m+[m[32m  }[m
[32m+[m[32m  #username_views {[m
[32m+[m[32m    display: none;[m
[32m+[m[32m  }[m
[32m+[m[32m}[m
 [m
  [m
  [m
[1mdiff --git a/Course/Content/Themes/Default/bootstrap.css b/Course/Content/Themes/Default/bootstrap.css[m
[1mindex 6d6e682..94653cd 100644[m
[1m--- a/Course/Content/Themes/Default/bootstrap.css[m
[1m+++ b/Course/Content/Themes/Default/bootstrap.css[m
[36m@@ -4076,7 +4076,7 @@[m [mtextarea.input-group-sm > .input-group-btn > .btn {[m
   clear: both;[m
 }[m
 [m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-header {[m
     float: left;[m
   }[m
[36m@@ -4116,7 +4116,7 @@[m [mtextarea.input-group-sm > .input-group-btn > .btn {[m
   overflow-y: auto;[m
 }[m
 [m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-collapse {[m
     width: auto;[m
     border-top: 0;[m
[36m@@ -4232,7 +4232,7 @@[m [mtextarea.input-group-sm > .input-group-btn > .btn {[m
   margin-top: 4px;[m
 }[m
 [m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-toggle {[m
     display: none;[m
   }[m
[1mdiff --git a/Course/Content/Themes/Readable/Site.css b/Course/Content/Themes/Readable/Site.css[m
[1mindex 1864dd7..13b6483 100644[m
[1m--- a/Course/Content/Themes/Readable/Site.css[m
[1m+++ b/Course/Content/Themes/Readable/Site.css[m
[36m@@ -16,6 +16,23 @@[m
     white-space: normal;[m
 }[m
 [m
[32m+[m[32m.shortLabel{[m
[32m+[m[32m    max-width:250px;[m
[32m+[m[32m    text-overflow:ellipsis;[m
[32m+[m[32m    display:block;[m
[32m+[m[32m    overflow:hidden;[m
[32m+[m[32m    white-space:nowrap;[m
[32m+[m[32m}[m
[32m+[m
[32m+[m[32m@media (max-width: 750px) {[m
[32m+[m[32m  #page_header {[m
[32m+[m[32m    display: none;[m
[32m+[m[32m  }[m
[32m+[m[32m  #username_views {[m
[32m+[m[32m    display: none;[m
[32m+[m[32m  }[m
[32m+[m[32m}[m
[32m+[m
 [m
  [m
  [m
[1mdiff --git a/Course/Content/Themes/Readable/bootstrap.css b/Course/Content/Themes/Readable/bootstrap.css[m
[1mindex 922c666..c7449eb 100644[m
[1m--- a/Course/Content/Themes/Readable/bootstrap.css[m
[1m+++ b/Course/Content/Themes/Readable/bootstrap.css[m
[36m@@ -4178,7 +4178,7 @@[m [mselect[multiple].input-group-sm > .input-group-btn > .btn {[m
     border-radius: 4px;[m
   }[m
 }[m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-header {[m
     float: left;[m
   }[m
[36m@@ -4195,7 +4195,7 @@[m [mselect[multiple].input-group-sm > .input-group-btn > .btn {[m
 .navbar-collapse.in {[m
   overflow-y: auto;[m
 }[m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-collapse {[m
     width: auto;[m
     border-top: 0;[m
[36m@@ -4319,7 +4319,7 @@[m [mselect[multiple].input-group-sm > .input-group-btn > .btn {[m
 .navbar-toggle .icon-bar + .icon-bar {[m
   margin-top: 4px;[m
 }[m
[31m-@media (min-width: 768px) {[m
[32m+[m[32m@media (min-width: 1200px) {[m
   .navbar-toggle {[m
     display: none;[m
   }[m
[1mdiff --git a/Course/Controllers/HomeController.cs b/Course/Controllers/HomeController.cs[m
[1mindex bb6c740..fb1991b 100644[m
[1m--- a/Course/Controllers/HomeController.cs[m
[1m+++ b/Course/Controllers/HomeController.cs[m
[36m@@ -24,7 +24,7 @@[m [mnamespace Course.Controllers[m
             var popularCreatives = db.Creatives.Include(x => x.ApplicationUser)[m
                                 .Include(x => x.Headers).OrderByDescending(u => u.Views).Take(5);[m
             var tags = db.Tags.ToList();[m
[31m-            return View(new HomeViewModels(lastCreatives, popularCreatives, tags));[m
[32m+[m[32m            return View(new HomeViewModel(lastCreatives, popularCreatives, tags));[m
         }[m
 [m
         public ActionResult HeadersByTag(int id)[m
[36m@@ -124,7 +124,7 @@[m [mnamespace Course.Controllers[m
             IncreaseViews(creative, isOwner);[m
             var userMark = GetUserMark(isOwner, id);[m
             [m
[31m-            return View(new ViewCreativeViewModels(creative, selectedHeaderId, userMark, isOwner));[m
[32m+[m[32m            return View(new ViewCreativeViewModel(creative, selectedHeaderId, userMark, isOwner));[m
         }[m
 [m
         private void IncreaseViews(Creative creative, bool isOwner)[m
[1mdiff --git a/Course/Controllers/UserController.cs b/Course/Controllers/UserController.cs[m
[1mindex 6faaa8d..902768d 100644[m
[1m--- a/Course/Controllers/UserController.cs[m
[1m+++ b/Course/Controllers/UserController.cs[m
[36m@@ -30,7 +30,7 @@[m [mnamespace Course.Controllers[m
             var user = db.Users.Find(currentUserId);[m
             var creatives = db.Creatives.Where(x => x.ApplicationUser.Id == currentUserId)[m
                                 .Include(x => x.ApplicationUser).Include(x => x.Headers);[m
[31m-            return View(new UserViewModels(creatives, user));[m
[32m+[m[32m            return View(new UserViewModel(creatives, user));[m
         }[m
 [m
         [HttpPost][m
[36m@@ -100,7 +100,7 @@[m [mnamespace Course.Controllers[m
             var tags = db.Tags.Select(x => x.Name).Distinct();[m
             var serializer = new JavaScriptSerializer();[m
 [m
[31m-            return View(new EditViewModels(creative, serializer.Serialize(tags)));[m
[32m+[m[32m            return View(new EditViewModel(creative, serializer.Serialize(tags)));[m
         }[m
 [m
         public void ChangeCreativeName(long Id, string Name)[m
[1mdiff --git a/Course/Course.csproj b/Course/Course.csproj[m
[1mindex c28736d..49f410d 100644[m
[1m--- a/Course/Course.csproj[m
[1m+++ b/Course/Course.csproj[m
[36m@@ -267,7 +267,6 @@[m
     <Compile Include="Models\Badge.cs" />[m
     <Compile Include="Models\Creative.cs" />[m
     <Compile Include="Models\CreativeResult.cs" />[m
[31m-    <Compile Include="Models\EditViewModels.cs" />[m
     <Compile Include="Models\Header.cs" />[m
     <Compile Include="Models\HomeViewModels.cs" />[m
     <Compile Include="Models\IdentityModels.cs" />[m
[36m@@ -275,7 +274,6 @@[m
     <Compile Include="Models\Tag.cs" />[m
     <Compile Include="Models\Rating.cs" />[m
     <Compile Include="Models\UserViewModels.cs" />[m
[31m-    <Compile Include="Models\ViewCreativeViewModels.cs" />[m
     <Compile Include="Properties\AssemblyInfo.cs" />[m
     <Compile Include="Resources\Resource.en.Designer.cs">[m
       <DependentUpon>Resource.en.resx</DependentUpon>[m
[36m@@ -304,6 +302,8 @@[m
     <Content Include="Content\Libs\jquery.tagit.css" />[m
     <Content Include="Content\Libs\simplemde.min.css" />[m
     <Content Include="Content\Libs\tagit.ui-zendesk.css" />[m
[32m+[m[32m    <Content Include="Content\Site\Login.css" />[m
[32m+[m[32m    <Content Include="Content\Site\Register.css" />[m
     <Content Include="Content\Site\Search.css" />[m
     <Content Include="Content\Themes\base\accordion.css" />[m
     <Content Include="Content\Themes\base\all.css" />[m
[36m@@ -371,6 +371,7 @@[m
     <Content Include="Content\fonts\fontawesome-webfont.woff" />[m
     <Content Include="Content\fonts\fontawesome-webfont.woff2" />[m
     <Content Include="Content\fonts\FontAwesome.otf" />[m
[32m+[m[32m    <None Include="Properties\PublishProfiles\alexmor.pubxml" />[m
     <None Include="Scripts\jquery-1.12.4.intellisense.js" />[m
     <Content Include="Scripts\jquery-1.12.4.js" />[m
     <Content Include="Scripts\jquery-1.12.4.min.js" />[m
[1mdiff --git a/Course/Course.csproj.user b/Course/Course.csproj.user[m
[1mindex 2cee891..19ea136 100644[m
[1m--- a/Course/Course.csproj.user[m
[1m+++ b/Course/Course.csproj.user[m
[36m@@ -12,6 +12,7 @@[m
     <WebStackScaffolding_IsAsyncSelected>False</WebStackScaffolding_IsAsyncSelected>[m
     <WebStackScaffolding_ViewDialogWidth>600</WebStackScaffolding_ViewDialogWidth>[m
     <WebStackScaffolding_DbContextTypeFullName>Course.Models.ApplicationDbContext</WebStackScaffolding_DbContextTypeFullName>[m
[32m+[m[32m    <NameOfLastUsedPublishProfile>alexmor</NameOfLastUsedPublishProfile>[m
   </PropertyGroup>[m
   <ProjectExtensions>[m
     <VisualStudio>[m
[1mdiff --git a/Course/Images/10-contornos.ai b/Course/Images/10-contornos.ai[m
[1mdeleted file mode 100644[m
[1mindex 0597934..0000000[m
[1m--- a/Course/Images/10-contornos.ai[m
[1m+++ /dev/null[m
[36m@@ -1,6747 +0,0 @@[m
[31m-%PDF-1.5%‚„œ”[m
[31m-1 0 obj<</Metadata 2 0 R/OCProperties<</D<</ON[5 0 R 137 0 R 138 0 R]/Order 139 0 R/RBGroups[]>>/OCGs[5 0 R 137 0 R 138 0 R]>>/Pages 3 0 R/Type/Catalog>>endobj2 0 obj<</Length 92682/Subtype/XML/Type/Metadata>>stream[m
[31m-<?xpacket begin="Ôªø" id="W5M0MpCehiHzreSzNTczkc9d"?>[m
[31m-<x:xmpmeta xmlns:x="adobe:ns:meta/" x:xmptk="Adobe XMP Core 5.3-c011 66.145661, 2012/02/06-14:56:27        ">[m
[31m-   <rdf:RDF xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#">[m
[31m-      <rdf:Description rdf:about=""[m
[31m-            xmlns:dc="http://purl.org/dc/elements/1.1/">[m
[31m-         <dc:format>application/pdf</dc:format>[m
[31m-         <dc:title>[m
[31m-            <rdf:Alt>[m
[31m-               <rdf:li xml:lang="x-default">10-contornos</rdf:li>[m
[31m-            </rdf:Alt>[m
[31m-         </dc:title>[m
[31m-      </rdf:Description>[m
[31m-      <rdf:Description rdf:about=""[m
[31m-            xmlns:xmp="http://ns.adobe.com/xap/1.0/"[m
[31m-            xmlns:xmpGImg="http://ns.adobe.com/xap/1.0/g/img/">[m
[31m-         <xmp:MetadataDate>2014-08-07T10:37:54+02:00</xmp:MetadataDate>[m
[31m-         <xmp:ModifyDate>2014-08-07T10:37:54+02:00</xmp:ModifyDate>[m
[31m-         <xmp:CreateDate>2014-07-22T10:02:47+02:00</xmp:CreateDate>[m
[31m-         <xmp:CreatorTool>Adobe Illustrator CS5</xmp:CreatorTool>[m
[31m-         <xmp:Thumbnails>[m
[31m-            <rdf:Alt>[m
[31m-               <rdf:li rdf:parseType="Resource">[m
[31m-                  <xmpGImg:width>256</xmpGImg:width>[m
[31m-                  <xmpGImg:height>256</xmpGImg:height>[m
[31m-                  <xmpGImg:format>JPEG</xmpGImg:format>[m
[31m-                  <xmpGImg:image>/9j/4AAQSkZJRgABAgEASABIAAD/7QAsUGhvdG9zaG9wIDMuMAA4QklNA+0AAAAAABAASAAAAAEA&#xA;AQBIAAAAAQAB/+4ADkFkb2JlAGTAAAAAAf/bAIQABgQEBAUEBgUFBgkGBQYJCwgGBggLDAoKCwoK&#xA;DBAMDAwMDAwQDA4PEA8ODBMTFBQTExwbGxscHx8fHx8fHx8fHwEHBwcNDA0YEBAYGhURFRofHx8f&#xA;Hx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8f/8AAEQgBAAEAAwER&#xA;AAIRAQMRAf/EAaIAAAAHAQEBAQEAAAAAAAAAAAQFAwIGAQAHC