<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>MyCreds Request Report</title>
        <style type="text/css">
          .main { text-align: center; }
          .container { margin-left: auto; margin-right: auto; width: 100%; text-align: left; }

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
                  <p class="heading0">MyCreds Request Report</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- ProgramApplicationReports-->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr class="heading1">
						<td class="labelBold cell" style="min-width: 70px;">Request ID</td>
						<td class="labelBold cell" style="min-width: 150px;">Full Name</td>
						<td class="labelBold cell" style="min-width: 60px;">sNumber</td>
						<td class="labelBold cell" style="min-width: 150px;">Email</td>
						<td class="labelBold cell" style="min-width: 70px;">Request Date</td>
                        <td class="labelBold cell" style="min-width: 70px;">Hold Type</td>
						<td class="labelBold cell" style="min-width: 70px;">Produced Date</td>
						<td class="labelBold cell" style="min-width: 70px;">Operator</td>
						<td class="labelBold cell" style="min-width: 250px;">Comments</td>
					</tr>
					<xsl:for-each select="MyCredsBatchTranscriptReports/TRRQRequestObj">
						<tr class="cell">
							<td>
								<xsl:value-of select="RequestID"/>
							</td>
							<td>
							    <xsl:value-of select="FullName"/>
						    </td>
						    <td>
							    <xsl:value-of select="sNumber"/>
						    </td>
						    <td>
							    <xsl:value-of select="Email"/>
						    </td>
							<td>
								<xsl:call-template name="formatDateOnly">
									<xsl:with-param name="DateTimeStr" select="RequestDate"/>
								</xsl:call-template>
							</td>
							<td>
                                <xsl:value-of select="HoldType"/>
                            </td>
							<td>
								<xsl:call-template name="formatDateOnly">
									<xsl:with-param name="DateTimeStr" select="DateProduced"/>
								</xsl:call-template>
							</td>
							<td>
							    <xsl:value-of select="Operator"/>
						    </td>
                            <td>
                                <xsl:value-of select="Comments"/>
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
