<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>OverduesReport</title>
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
                  <p class="heading0">Overdues Report</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- OverduesReports-->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr class="heading1">
					    <td class="labelBold cell" style="min-width: 50px;">Person ID</td>
					    <td class="labelBold cell" style="min-width: 190px;">LName, FName, MName</td>
					    <td class="labelBold cell" style="min-width: 50px;">Alien Status</td>
					    <td class="labelBold cell" style="min-width: 50px;">Status</td>
					    <td class="labelBold cell" style="min-width: 50px;">Program</td>
					    <td class="labelBold cell" style="min-width: 50px;">Cond. Status</td>
					    <td class="labelBold cell" style="min-width: 80px;">Phone1</td>
					    <td class="labelBold cell" style="min-width: 50px;">Type1</td>
					    <td class="labelBold cell" style="min-width: 80px;">Phone2</td>
					    <td class="labelBold cell" style="min-width: 50px;">Type2</td>
					    <td class="labelBold cell" style="min-width: 50px;">Deadline</td>
                        <td class="labelBold cell" style="min-width: 50px;">Start Term</td>
                        <td class="labelBold cell" style="min-width: 50px;">Location</td>
                        <td class="labelBold cell" style="min-width: 150px;">Comments</td>
                    </tr>
					<xsl:for-each select="OverduesReports/CollOverduesReportSearchResultsFilter">
					    <tr class="cell">
						    <td>
							    <xsl:value-of select="ID"/>
						    </td>
						    <td>
							    <xsl:value-of select="FullName"/>
						    </td>
						    <td>
							    <xsl:value-of select="AlienStatus"/>
						    </td>
						    <td>
							    <xsl:value-of select="Status"/>
						    </td>
						    <td>
							    <xsl:value-of select="CondProgram"/>
						    </td>
                            <td>
                                <xsl:value-of select="CondStatus"/>
                            </td>
                            <td>
								<xsl:value-of select="Phone1"/>
							</td>
							<td>
								<xsl:value-of select="Type1"/>
							</td> 
							<td>
								<xsl:value-of select="Phone2"/>
							</td>
							<td>
								<xsl:value-of select="Type2"/>
							</td>
							<td>
								<xsl:call-template name="formatDateOnly">
									<xsl:with-param name="DateTimeStr" select="Deadline"/>
								</xsl:call-template>
							</td>
							<td>
								<xsl:value-of select="StartTerm"/>
							</td>
                            <td>
                                <xsl:value-of select="Location"/>
                            </td>
                            <td>
                                <xsl:value-of select="Comments"/>
                            </td>
                        </tr>
					</xsl:for-each>
                    <!-- Loopg through error messages -->
                    <tr>
                      <td colspan="4" class="heading2"></td>
                    </tr>
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
