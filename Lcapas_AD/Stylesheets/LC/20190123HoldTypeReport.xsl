<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Hold Type Report</title>
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

          tr.yellow { background-color: #ffff78; }

          .request-legend { font: 12px Arial; float: left; width: 425px; border: 1px solid lightgray; border-radius: 5px; margin: 0 0 0 15px; padding: 1px 10px 1px 10px; }
          .request-status { max-height: 22px; max-width: 22px; margin: 0px 5px -5px 2px; }

        </style>
      </head>
      <body>
        <div class="main">
          <div class="container">
            <table class="table1" cellpadding="0" cellspacing="0" border="0">
              <tr>
                <td>
                  <p class="heading0">HoldType Reports</p> 
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>

                  <!-- HoldTypeReports-->
           
              <tr>
                <td>
                  <table class="table2" cellspacing="0" cellpadding="2">
                    <tr>
						<td class="labelBold cell" style="width: 150px;">sNumber</td>
            <td class="labelBold cell" style="width: 300px;">Full Name</td>
						<td class="labelBold cell" style="width: 150px;">ASN</td>
            <xsl:choose>
              <xsl:when test="HoldTypeReports/HoldTypeReportSearchResultsFilter/DestAction = 'AdmissionsOutbox'">
                <td class="labelBold cell" style="width: 600px;">To Fulfilling Institution</td>
              </xsl:when>
              <xsl:otherwise>
                <td class="labelBold cell" style="width: 600px;">From Request Institution</td>
              </xsl:otherwise>
            </xsl:choose>
						<td class="labelBold cell" style="width: 150px;">Colleague Status</td>
						<td class="labelBold cell" style="width: 150px;">Hold Type</td>
						<td class="labelBold cell" style="width: 200px;">Hold Type Data</td>
						<td class="labelBold cell" style="width: 150px;">Response Status</td>
						<td class="labelBold cell" style="width: 150px;">Operator</td>
						<td class="labelBold cell" style="width: 150px;">Status Date</td>
            <td class="labelBold cell"></td>
					</tr>
					  <xsl:for-each select="HoldTypeReports/HoldTypeReportSearchResultsFilter">
							 <tr class="cell">
                 <xsl:if test="ViewedDateTime = ''">
                   <xsl:attribute name="class">cell yellow</xsl:attribute>
                 </xsl:if>
							 	  <td>
									  <xsl:value-of select="SNumber"/>
								  </td>
								   <td>
									  <xsl:value-of select="FullName"/>
								  </td>
								  <td>
									  <xsl:value-of select="ASN"/>
								  </td>
								  <td>
									  <xsl:value-of select="FromRequestingInstitution"/>
								  </td>
								    <td>
									  <xsl:value-of select="ColleagueStatus"/>
								  </td>
								  <td>
									  <xsl:value-of select="HoldType"/>
								  </td>
								  <td>
									  <xsl:value-of select="HoldTypeData"/>
								  </td>
								  <td>
									  <xsl:value-of select="ResponseStatus"/>
								  </td>
								  <td>
									  <xsl:value-of select="Operator"/>
								  </td>
								   <td>
									  <xsl:call-template name="formatDateOnly">
										  <xsl:with-param name="DateTimeStr" select="FromStatus"/>
									  </xsl:call-template>
								  </td>
                 <td>
                   <xsl:choose>
                     <xsl:when test="TranscriptCount > 0 or (ResponseCount > 0 and (ResponseStatus = 'Canceled' or ResponseStatus = 'OfflineRecordSent'))">
                       <img src="/Content/Images/bullet_ball_glass_green.png" class="request-status" />
                     </xsl:when>
                     <xsl:otherwise>
                       <xsl:choose>
                         <xsl:when test="ResponseCount > 0">
                           <img src="/Content/Images/bullet_ball_glass_yellow.png" class="request-status" />
                         </xsl:when>
                         <xsl:otherwise>
                           <xsl:choose>
                             <xsl:when test="ErrorMessageCount > 0">
                               <img src="/Content/Images/bullet_ball_glass_red.png" class="request-status" />
                             </xsl:when>
                             <xsl:otherwise>
                             </xsl:otherwise>
                           </xsl:choose>
                         </xsl:otherwise>
                       </xsl:choose>
                     </xsl:otherwise>
                   </xsl:choose>
                 </td>
                 <xsl:if test="DestAction = 'RecordsInbox'">
                   <td>
                     <xsl:if test="SentTRRQ != ''">
                       <img src="/Content/Images/check.png" class="request-status" />
                     </xsl:if>
                   </td>
                 </xsl:if>
               </tr>
					  </xsl:for-each>
                    
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
             
            </table>

            <br />
            <div class="request-legend">
              <label>Legend: </label>
              <img src="/Content/Images/bullet_ball_glass_green.png" class="request-status" />
              <label class="legend-item">Transcript Sent</label>
              <img src="/Content/Images/bullet_ball_glass_yellow.png" class="request-status" />
              <label class="legend-item">Response Sent</label>
              <img src="/Content/Images/bullet_ball_glass_red.png" class="request-status" />
              <label class="legend-item">Error Message</label>
            </div>

          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
