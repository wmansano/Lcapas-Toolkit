<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>DailyStudentRequests</title>
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
                  <p class="heading0">Daily Student Requests</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- DailyStudentRequests -->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr class="heading1">
						<td class="labelBold cell" style="width: 150px;">Full Name</td>
						<td class="labelBold cell" style="width: 150px;">Type</td>
						<td class="labelBold cell" style="width: 150px;">Date</td>
						<td class="labelBold cell" style="width: 150px;">Comments</td>
						<td class="labelBold cell" style="width: 150px;">RecpID</td>
						<td class="labelBold cell" style="width: 150px;">Recp Name</td>
						<td class="labelBold cell" style="width: 150px;">Street</td>
						<td class="labelBold cell" style="width: 150px;">City</td>
						<td class="labelBold cell" style="width: 150px;">Province</td>
						<td class="labelBold cell" style="width: 150px;">PostalCode</td>
						<td class="labelBold cell" style="width: 150px;">AddDate</td>
                     
                    </tr>
					  <xsl:for-each select="DailyStudentRequests/DailyRequestReportSearchResultsFilter">
							  <tr class="cell">
								  <td  style="width: 150px;">
									  <xsl:value-of select="FullName"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="Type"/>
								  </td>
								  <td style="width: 150px;">
									  <xsl:call-template name="formatDateOnly">
										  <xsl:with-param name="DateTimeStr" select="FromDate"/>
									  </xsl:call-template>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="Comments"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="RecpID"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="RecpFullName"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="Street"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="City"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="Prv"/>
								  </td>
								  <td  style="width: 150px;">
									  <xsl:value-of select="PCode"/>
								  </td>
								  <td style="width: 150px;">
									  <xsl:call-template name="formatDateOnly">
										  <xsl:with-param name="DateTimeStr" select="AddDt"/>
									  </xsl:call-template>
								  </td>
							  </tr>
					  </xsl:for-each>
                    <!-- Loopg through error messages -->
                    <tr>
                      <td colspan="4" class="heading2"></td>
                    </tr>
                    <xsl:for-each select="ValidationErrors/ValidationError">
                      <tr>
                        <td class="labelBold" style="width: 50px;">Error Code:</td>
                        <td class="label">
                          <xsl:value-of select="ErrorCode"/>
                        </td>
                      </tr>
                      <tr>
                        <td class="labelBold cell">Error Text:</td>
                        <td class="label cell">
                          <xsl:value-of select="ErrorText"/>
                        </td>
                      </tr>
                    </xsl:for-each>
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
