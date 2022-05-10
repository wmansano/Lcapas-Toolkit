<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Transfer Credits</title>
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
                  <p class="heading0">Transfer Credits</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

              <!-- TransferCredits -->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr class="heading1">
                      <td class="labelBold cell" style="width: 100px;">ASN</td>
                      <td class="labelBold cell" style="width: 100px;">BirthDate</td>
					  <td class="labelBold cell" style="width: 100px;">ToInstitution</td>
					  <td class="labelBold cell" style="width: 200px;">FromInstitutionLoc</td>
					  <td class="labelBold cell" style="width: 200px;">FromInstitutionCourseCode</td>
					  <td style="width: 100px;">
						  <xsl:call-template name="formatDateOnly">
							  <xsl:with-param name="DateTimeStr" select="FromInstitutionCourseDate"/>
						  </xsl:call-template>
					  </td>
                      <td class="labelBold cell" style="width: 100px;">ToInstitutionCourseCode</td>
                      <td class="labelBold cell" style="width: 100px;">TCA</td>
					  <td class="labelBold cell" style="width: 100px;">TCAType</td>
                      <td style="width: 100px;">
						  <xsl:call-template name="formatDateOnly">
							  <xsl:with-param name="DateTimeStr" select="TCADate"/>
						  </xsl:call-template>
				      </td>
					  <td class="labelBold cell" style="width: 100px;">Grade</td>
                    </tr>
					  <xsl:for-each select="TransferCredits/TransferCreditSearchResultsFilter">
						  <tr class="cell">
							   <td  style="width: 100px;">
								  <xsl:value-of select="ASN"/>
							   </td>
							   <td style="width: 100px;">
								  <xsl:call-template name="formatDateOnly">
									  <xsl:with-param name="DateTimeStr" select="BirthDate"/>
								  </xsl:call-template>
							   </td>
							   <td  style="width: 200px;">
								  <xsl:value-of select="ToInstitution"/>
							   </td>
							   <td  style="width: 200px;">
								  <xsl:value-of select="FromInstitutionLoc"/>
							   </td>
							   <td  style="width: 150px;">
								  <xsl:value-of select="FromInstitutionCourseCode"/>
							   </td>
							   <td style="width: 100px;">
								  <xsl:call-template name="formatDateOnly">
									  <xsl:with-param name="DateTimeStr" select="FromInstitutionCourseDate"/>
								  </xsl:call-template>
							   </td>
							   <td  style="width: 100px;">
								  <xsl:value-of select="ToInstitutionCourseCode"/>
							   </td>
							   <td  style="width: 150px;">
								  <xsl:value-of select="TCA"/>
							   </td>
							   <td  style="width: 150px;">
								  <xsl:value-of select="TCAType"/>
							   </td>
							   <td style="width: 100px;">
								  <xsl:call-template name="formatDateOnly">
									  <xsl:with-param name="DateTimeStr" select="TCADate"/>
								  </xsl:call-template>
							  </td>
							  <td  style="width: 150px;">
								  <xsl:value-of select="Grade"/>
							  </td>
			  
						  </tr>
					  </xsl:for-each>
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
