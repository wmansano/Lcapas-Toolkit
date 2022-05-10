<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>ApplicationReport</title>
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
                  <p class="heading0">Application Reports</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- Toolkit Application Report -->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr class="heading1">
                      <td class="labelBold cell" style="min-width: 130px;">Full Name</td>
											<td class="labelBold cell" style="min-width: 60px;">Program</td>
											<td class="labelBold cell" style="min-width: 30px;">Term</td>
											<td class="labelBold cell" style="min-width: 40px;">Campus</td>
                      <td class="labelBold cell" style="min-width: 50px;">sNumber</td>
                      <td class="labelBold cell" style="min-width: 50px;">ASN</td>
                      <td class="labelBold cell" style="min-width: 50px;">Language</td>
											<td class="labelBold cell" style="min-width: 40px;">Admit Status</td>
                      <td class="labelBold cell" style="min-width: 30px;">Study Load</td>
                      <td class="labelBold cell" style="min-width: 100px;">Reference Desc</td>
                      <td class="labelBold cell" style="min-width: 60px;">Prev. Applied</td>
                      <td class="labelBold cell" style="min-width: 50px;">Disability</td>
											<td class="labelBold cell" style="min-width: 60px;">Application ID</td>
											<td class="labelBold cell" style="min-width: 60px;">From Date</td>
                      <td class="labelBold cell" style="min-width: 60px;">Received Date</td>
                      <td class="labelBold cell" style="min-width: 60px;">Paid Date</td>
                      <td class="labelBold cell" style="min-width: 50px;">Prev. sNumber</td>
                      <td class="labelBold cell" style="min-width: 60px;">Address Line 1</td>
                      <td class="labelBold cell" style="min-width: 60px;">Address Line 2</td>
                      <td class="labelBold cell" style="min-width: 60px;">City</td>
                      <td class="labelBold cell" style="min-width: 60px;">Province</td>
                      <td class="labelBold cell" style="min-width: 60px;">Postal Code</td>
                      <td class="labelBold cell" style="min-width: 60px;">Country</td>
                      <td class="labelBold cell" style="min-width: 60px;">Phone</td>
                      <td class="labelBold cell" style="min-width: 60px;">Gender</td>
                      <td class="labelBold cell" style="min-width: 60px;">Birth Date</td>
                      <td class="labelBold cell" style="min-width: 60px;">Ethnicity Race</td>
                      <td class="labelBold cell" style="min-width: 60px;">First Entry Into Country</td>
                      <td class="labelBold cell" style="min-width: 60px;">Family Attended College</td>
                      <td class="labelBold cell" style="min-width: 60px;">Email Address</td>
                    </tr>
									<xsl:for-each select="ApplicationReports/ApplicationReportSearchResultsFilter">
											<tr class="cell">
												 <td>
													<xsl:value-of select="FullName"/>
												</td>
												<td>
													<xsl:value-of select="ProgramCode"/>
												</td>
												<td>
													<xsl:value-of select="TermCode"/>
												</td>
												<td>
													<xsl:value-of select="CampusCode"/>
												</td>
                        <td>
                          <xsl:value-of select="sNumber"/>
                        </td>
                        <td>
													<xsl:value-of select="AgencyAssignedID"/>
												</td>
												<td>
                          <xsl:value-of select="LanguageCode"/>
                        </td>
                        <td>
                          <xsl:value-of select="AdmitStatus"/>
                        </td>
                        <td>
                          <xsl:value-of select="StudyLoad"/>
                        </td>
                        <td>
                          <xsl:value-of select="ReferenceTypeDesc"/>
                        </td>
                        <td>
                          <xsl:value-of select="PreviouslyApplied"/>
                        </td>
                        <td>
                          <xsl:value-of select="Disability"/>
                        </td>
                        <td>
                          <xsl:value-of select="ApplicationID" />
                        </td>
                        <td>
                          <xsl:call-template name="formatDateOnly">
                            <xsl:with-param name="DateTimeStr" select="FromDate"/>
                          </xsl:call-template>
                        </td>
                        <td>
                          <xsl:call-template name="formatDateOnly">
                            <xsl:with-param name="DateTimeStr" select="ReceivedDateTime"/>
                          </xsl:call-template>
                        </td>
                        <td>
                          <xsl:call-template name="formatDateOnly">
                            <xsl:with-param name="DateTimeStr" select="PaidDateTime"/>
                          </xsl:call-template>
                        </td>
                        <td>
                          <xsl:value-of select="PrevSNumber"/>
                        </td>
                        <td>
                          <xsl:value-of select="AddressLine1"/>
                        </td>
                        <td>
                          <xsl:value-of select="AddressLine2"/>
                        </td>
                        <td>
                          <xsl:value-of select="City"/>
                        </td>
                        <td>
                          <xsl:value-of select="Province"/>
                        </td>
                        <td>
                          <xsl:value-of select="PostalCode"/>
                        </td>
                        <td>
                          <xsl:value-of select="Country"/>
                        </td>
                        <td>
                          <xsl:value-of select="AreaCode"/>
                          <xsl:value-of select="PhoneNumber"/>
                        </td>
                        <td>
                          <xsl:value-of select="Gender"/>
                        </td>
                        <td>
                          <xsl:call-template name="formatDateOnly">
                            <xsl:with-param name="DateTimeStr" select="BirthDate"/>
                          </xsl:call-template>
                        </td>
                        <td>
                          <xsl:value-of select="EthnicityRace"/>
                        </td>
                        <td>
                          <xsl:call-template name="formatDateOnly">
                            <xsl:with-param name="DateTimeStr" select="FirstEntryIntoCountry"/>
                          </xsl:call-template>
                        </td>
                        <td>
                          <xsl:value-of select="FamilyAttendedCollege"/>
                        </td>
                        <td>
                          <xsl:value-of select="EmailAddress"/>
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
