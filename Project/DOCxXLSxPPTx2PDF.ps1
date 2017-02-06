
$folderpath =  $(get-location) 
 Add-type -AssemblyName office
#Convert Word formats to pdf
$wdFormatPDF = 17
 $word = New-Object -ComObject word.application
 $word.visible = $false
 $fileTypes = "*.docx","*doc"
 $wordFiles = Get-ChildItem -path $folderpath -include $fileTypes -Recurse
foreach ($d in $wordFiles) {
 $path = ($d.fullname).substring(0,($d.FullName).lastindexOf("."))
 "Converting $path to pdf ..."
 $doc = $word.documents.open($d.fullname)
 $doc.saveas([ref] $path, [ref]$wdFormatPDF)
 $doc.close()
 }
$word.Quit()
#Convert Excel formats to pdf
$xlTypePDF = 0 # Microsoft.Office.Interop.Excel.XlFixedFormatType
 $xlQualityStandard = 0 # Microsoft.Office.Interop.Excel.XlFixedFormatQuality
$excelFiles = Get-ChildItem -Path $folderpath -include *.xls, *.xlsx -recurse
 $objExcel = New-Object -ComObject excel.application
 $ci = [System.Globalization.CultureInfo]'en-US'
$objExcel.visible = $false
 foreach($wb in $excelFiles) {
 $filepath = Join-Path -Path $folderpath -ChildPath ($wb.BaseName + ".pdf")
$workbook = $objExcel.Workbooks.PSBase.GetType().InvokeMember('Open', [Reflection.BindingFlags]::InvokeMethod, $null, $objExcel.Workbooks, $wb.fullname, $ci)
 "saving $filepath"
 $workbook.ExportAsFixedFormat($xlTypePDF, $filepath, $xlQualityStandard)
[void]$workbook.PSBase.GetType().InvokeMember('Close', [Reflection.BindingFlags]::InvokeMethod, $null, $workbook, 0, $ci)
 }
$objExcel.Quit()
#Convert Powerpoint formats to pdf
$ppFormatPDF = 2
 $ppQualityStandard = 0
 $p = new-object -comobject powerpoint.application
$p.visible = [Microsoft.Office.Core.MsoTriState]::msoTrue
 $ppFiletypes = "*.pptx","*ppt"
 $ppFiles = Get-ChildItem -path $folderpath -include $ppFiletypes -Recurse
foreach ($s in $ppFiles) {
 $pppath = ($s.fullname).substring(0,($s.FullName).lastindexOf("."))
 "Converting $pppath to pdf ..."
 $ppt = $p.presentations.open($s.fullname)
$ppt.SavecopyAs($pppath, 32) # 32 is for PDF
 $ppt.close()
 }
$p.Quit()
 $p = $null
[gc]::collect()
 [gc]::WaitForPendingFinalizers()
