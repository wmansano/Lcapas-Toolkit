<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Sent Emails Report</title>
        <style type="text/css">
          .main { text-align: center; }
          .container { margin-left: auto; margin-right: auto; width: 990px; text-align: left; }

          .table1 { border: 0px solid #dddddd; width: 100%; }
          .table2 { border: 1px solid #999999; width: 100%; }

          .label { font: 12px Arial; }
          .labelRed { font: bold 14px Arial; color: red; }
          .cell { font: 12px Arial; border-bottom: 1px solid #ebebeb; }
          .labelBold { font: bold 12px Arial; }
          .bottom-line { border-bottom: 1px solid #ebebeb; }

          .heading0 { background: none; font: normal 20px Arial;  border-bottom: 1px solid #999966; color: #78A22F; }
          .heading1 { background: none; font: normal 14px Verdana; border-bottom: 1px solid #bbbb88; color: #78A22F; }
          .heading2 { border-bottom: 1px solid #bbbbbb; font :bold 12px Verdana; background: none; color: #666666; }
          .print { width:99%;text-align:right; margin:5px; }
		  
        </style>
      </head>
      <body>
        <div class="main">
          <div class="container">
            <table class="table1" cellpadding="0" cellspacing="0" border="0">
              <tr>
                <td>
                  <p class="heading0">Sent Email Reports</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- SentEmailReports-->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <thead>
                      <tr class="heading1">
						<td class="labelBold cell" style="width: 100px;">Email Type</td>
						<td class="labelBold cell" style="width: 150px;">Subject</td>
						<td class="labelBold cell" style="width: 100px;">Body</td>
						<td class="labelBold cell" style="width: 250px;">From</td>
						<td class="labelBold cell" style="width: 150px;">To</td>
						<td class="labelBold cell" style="width: 150px;">Created By</td>
						<td class="labelBold cell" style="width: 150px;">Created DateTime</td>
						<td class="labelBold cell" style="width: 150px;">Modified By</td>
						<td class="labelBold cell" style="width: 150px;">Modified DateTime</td>
                      </tr>
                    </thead>
                    <tbody>
					    <xsl:for-each select="SentEmailReports/SentEmailReportSearchResultsFilter">
						    <tr class="cell">
							    <td  style="width: 100px;">
								    <xsl:value-of select="EmailType"/>
							    </td>
							    <td  style="width: 150px;">
								    <xsl:value-of select="Subject"/>
							    </td>
							    <td  style="width: 100px;">
								    <xsl:value-of select="Body"/>
							    </td>
							    <td  style="width: 250px;">
								    <xsl:value-of select="From"/>
							    </td>
							    <td  style="width: 150px;">
								    <xsl:value-of select="To"/>
							    </td>
							    <td  style="width: 150px;">
								    <xsl:value-of select="CreatedBy"/>
							    </td>
							    <td style="width: 150px;">
								    <xsl:call-template name="formatDateOnly">
									    <xsl:with-param name="DateTimeStr" select="CreatedDateTime"/>
								    </xsl:call-template>
							    </td>
							    <td  style="width: 150px;">
								    <xsl:value-of select="ModifiedBy"/>
							    </td>
							    <td style="width: 150px;">
								    <xsl:call-template name="formatDateOnly">
									    <xsl:with-param name="DateTimeStr" select="ModifiedDateTime"/>
								    </xsl:call-template>
							    </td>
						    </tr>
					    </xsl:for-each>
                    </tbody>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
            </table>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
