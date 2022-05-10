<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:tr="urn:org:pesc:message:TranscriptResponse:v1.4.0"  exclude-result-prefixes="tr">
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Transcript Response</title>
        <style type="text/css">
          .mainLC { text-align: center; }
          .containerLC { margin-left: auto; margin-right: auto; width: 650px; text-align: left; }

          .tableMainLC { border: 0px solid #dddddd; width: 100%; background: none; }
          .tableMainLC td { border: 0px; }
          .tableSecondLC { border: 1px solid #999999; width: 100%; background: none; }
          .tableThirdLC { border: 0px; width: 100%; background: none; }

          .labelBoldLC { font: bold 11px Arial; border: 0px; }
          .labelLC { font: 12px Arial; border: 0px; }
          .labelRedLC { font: bold 14px Arial; color: red; border: 0px; }

          .tableMainLC td.heading0LC { font: bold 20px Arial; background: none; border: 0px; border-bottom: 1px solid #999966; padding: 5px; }
          .tableMainLC td.heading1LC { font: bold 14px Verdana; background: none; border: 0px; border-bottom: 1px solid #bbbb88; color: #777700; }
          .tableSecondLC td.heading2LC { font: bold 12px Verdana; background: none; border: 0px; border-bottom: 1px solid #bbbbbb; color: #666666; }

          @media screen { .page-breakLC { margin: 15px 0 20px; border-bottom: 1px dashed #8c8b8b; }}
          @media print { .page-breakLC { page-break-before: always; margin-bottom: 20px; }}
        </style>
      </head>   
      <body>
        <div class="mainLC">
          <div class="containerLC">
            <xsl:for-each select=".//tr:TranscriptResponse">

              <table class="tableMainLC page-breakLC" cellpadding="0" cellspacing="0" border="0">
              <tr>
                <td class="heading0LC" align="center">
                  Transcript Response
                  <xsl:if test="tr:TransmissionData/DocumentProcessCode = 'TEST'">
                    <div class="labelRedLC" align="center">
                      <br/><xsl:value-of select="tr:TransmissionData/DocumentProcessCode"/>
                    </div>
                  </xsl:if>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Response -->
              <tr><td class="heading1LC">Response Information</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellspacing="0" cellpadding="2">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Response:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:Response/ResponseStatus"/></td>
                      <td class="labelBoldLC" style="width: 125px;"></td>
                      <td class="labelLC" style="width: 200px;"></td>
                    </tr>

                    <!-- Hold Reasons -->
                    <xsl:if test="tr:Response/ResponseHold">
                      <tr>
                        <td class="labelBoldLC">Hold Reason:</td>
                        <td class="labelLC" colspan="3">
                          <xsl:for-each select="tr:Response/ResponseHold">
                            <xsl:value-of select="./HoldReason"/>
                          </xsl:for-each>
                        </td>
                      </tr>
                    </xsl:if>

                    <tr>
                      <td class="labelBoldLC" valign="top">Comments:</td>
                      <td class="labelLC" colspan="3">
                        <xsl:for-each select="tr:Response/NoteMessage">
                          <xsl:value-of select="."/><br/>
                        </xsl:for-each>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Student -->
              <tr><td class="heading1LC">Student Information</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellspacing="0" cellpadding="2">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">ASN:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="tr:Response/RequestedStudent/Person/AgencyIdentifier/AgencyAssignedID"/></td>
                      <td class="labelBoldLC" style="width: 145px;">Recipient Assigned ID:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:Response/RequestedStudent/Person/RecipientAssignedID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">School Assigned ID:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/SchoolAssignedPersonID"/></td>
                      <td class="labelBoldLC">Last High School:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/HighSchool/OrganizationName"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Gender:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/Gender/GenderCode"/></td>
                      <td class="labelBoldLC">Birthdate:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/Birth/BirthDate"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">First Name:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/Name/FirstName"/></td>
                      <td class="labelBoldLC">Last Name:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Person/Name/LastName"/></td>
                    </tr>
                    <xsl:if test="tr:Response/RequestedStudent/Person/Name/MiddleName">
                      <tr>
                        <td class="labelBoldLC">Middle Name(s)</td>
                        <td class="labelLC" colspan="3">
                          <xsl:for-each select="tr:Response/RequestedStudent/Person/Name/MiddleName">
                            <xsl:value-of select="."/><xsl:if test="position() != last()">,</xsl:if>
                          </xsl:for-each>
                        </td>
                      </tr>
                    </xsl:if>
                    
                    <!-- Alternate Names -->
                    <xsl:if test="tr:Response/RequestedStudent/Person/Name/AlternateName">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <tr><td colspan="4" class="labelBoldLC">Former Names</td></tr>
                      <xsl:for-each select="tr:Response/RequestedStudent/Person/AlternateName">
                        <tr>
                          <td class="labelBoldLC">First Name:</td>
                          <td class="labelLC"><xsl:value-of select="FirstName"/></td>
                          <td class="labelBoldLC">Last Name:</td>
                          <td class="labelLC"><xsl:value-of select="LastName"/></td>
                        </tr>
                        <xsl:if test="MiddleName">
                          <tr>
                            <td class="labelBoldLC">Middle Name(s):</td>
                            <td class="labelLC">
                              <xsl:for-each select="MiddleName">
                                <xsl:value-of select="."/>
                                <xsl:if test="position() != last()">,</xsl:if>
                              </xsl:for-each>
                            </td>
                          </tr>
                        </xsl:if>
                      </xsl:for-each>
                    </xsl:if>
                    
                    <!-- Contacts -->
                    <xsl:if test="tr:Response/RequestedStudent/Person/Contacts/Address">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="tr:Response/RequestedStudent/Person/Contacts/Address">
                        <tr>
                          <td class="heading2LC" colspan="4">Address</td>
                        </tr>
                        <xsl:for-each select="AddressLine">
                          <tr>
                            <td class="labelBoldLC">Address Line:</td>
                            <td class="labelLC" colspan="3"><xsl:value-of select="."/></td>
                          </tr>
                        </xsl:for-each>
                        <tr>
                          <td class="labelBoldLC">City:</td>
                          <td class="labelLC"><xsl:value-of select="City"/></td>
                          <xsl:if test="StateProvinceCode">
                            <td class="labelBoldLC">State/Province Code:</td>
                            <td class="labelLC"><xsl:value-of select="StateProvinceCode"/></td>
                          </xsl:if>
                          <xsl:if test="StateProvince">
                            <td class="labelBoldLC">State/Province:</td>
                            <td class="labelLC"><xsl:value-of select="StateProvince"/></td>
                          </xsl:if>
                        </tr>
                        <tr>
                          <td class="labelBoldLC">Postal Code:</td>
                          <td class="labelLC"><xsl:value-of select="PostalCode"/></td>
                          <td class="labelBoldLC">Country Code:</td>
                          <td class="labelLC"><xsl:value-of select="CountryCode"/></td>
                        </tr>
                      </xsl:for-each>
                    </xsl:if>
                    
                    <!-- Phones -->
                    <xsl:if test="tr:Response/RequestedStudent/Person/Contacts/Phone">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="tr:Response/RequestedStudent/Person/Contacts/Phone">
                        <tr>
                          <td class="heading2LC" colspan="4">Phone</td>
                        </tr>
                        <tr>
                          <td class="labelBoldLC">Country Code:</td>
                          <td class="labelLC"><xsl:value-of select="CountryPrefixCode"/></td>
                          <td class="labelBoldLC">Area Code:</td>
                          <td class="labelLC"><xsl:value-of select="AreaCityCode"/></td>
                        </tr>
                        <tr>
                          <td class="labelBoldLC">Phone Number:</td>
                          <td class="labelLC"><xsl:value-of select="PhoneNumber"/></td>
                          <td class="labelBoldLC">Extension:</td>
                          <td class="labelLC"><xsl:value-of select="PhoneNumberExtension"/></td>
                        </tr>
                      </xsl:for-each>
                    </xsl:if>
                    
                    <!-- Emails -->
                    <xsl:if test="tr:Response/RequestedStudent/Person/Contacts/Email">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="tr:Response/RequestedStudent/Person/Contacts/Email">
                        <tr>
                          <td class="heading2LC" colspan="4">Email</td>
                        </tr>
                        <tr>
                          <td class="labelBoldLC">Email Address:</td>
                          <td class="labelLC" colspan="3"><xsl:value-of select="EmailAddress"/></td>
                        </tr>
                      </xsl:for-each>
                    </xsl:if>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Academic History -->
              <tr><td class="heading1LC">Academic History</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellspacing="0" cellpadding="2">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Institution:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="tr:Response/RequestedStudent/Attendance/School/OrganizationName"/></td>
                      <td class="labelBoldLC" style="width: 145px;">Current Enrollment:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:Response/RequestedStudent/Attendance/CurrentEnrollmentIndicator"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Enrollment Date:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Attendance/EnrollDate"/></td>
                      <td class="labelBoldLC">Exit Date:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestedStudent/Attendance/ExitDate"/></td>
                    </tr>
                    
                    <!-- Academic Awards -->
                    <xsl:if test="tr:Response/RequestedStudent/Attendance/AcademicAwardsReported">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="tr:Response/RequestedStudent/Attendance/AcademicAwardsReported">
                        <tr>
                          <td class="heading2LC" colspan="4">Academic Award</td>
                        </tr>
                        <tr>
                          <td class="labelBoldLC">Title:</td>
                          <td class="labelLC"><xsl:value-of select="AcademicAwardTitle"/></td>
                          <td class="labelBoldLC">Date:</td>
                          <td class="labelLC"><xsl:value-of select="AcademicAwardDate"/></td>
                        </tr>
                      </xsl:for-each>
                    </xsl:if>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Release Authorization -->
              <tr><td class="heading1LC">Release Authorization</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellspacing="0" cellpadding="2">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Authorized:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="tr:Response/RequestedStudent/ReleaseAuthorizedIndicator"/></td>
                      <td class="labelBoldLC" style="width: 145px;">Method:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:Response/RequestedStudent/ReleaseAuthorizedMethod"/></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Transmission Data -->
              <tr><td class="heading1LC">Transmission Data</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellspacing="0" cellpadding="2">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Document Type:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:TransmissionData/DocumentTypeCode"/></td>
                      <td class="labelBoldLC" style="width: 125px;">Document ID:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="tr:TransmissionData/DocumentID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Created Date:</td>
                      <td class="labelLC">
                        <xsl:call-template name="formatdate">
                          <xsl:with-param name="DateTimeStr" select="tr:TransmissionData/CreatedDateTime"/>
                        </xsl:call-template>
                      </td>
                      <td class="labelBoldLC">Request Tracking ID:</td>
                      <td class="labelLC"><xsl:value-of select="tr:Response/RequestTrackingID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Transmission Type:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="tr:TransmissionData/TransmissionType"/></td>
                      <td class="labelBoldLC" style="width: 145px;">Process Code:</td>
                      <td class="labelLC" style="width: 200px;">
                        <xsl:choose>
                          <xsl:when test="tr:TransmissionData/DocumentProcessCode != ''">
                            <xsl:value-of select="tr:TransmissionData/DocumentProcessCode"/>
                          </xsl:when>
                          <xsl:otherwise>
                            PRODUCTION
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Official Code:</td>
                      <td class="labelLC" style="width: 180px;">
                        <xsl:choose>
                          <xsl:when test="tr:TransmissionData/DocumentOfficialCode != ''">
                            <xsl:value-of select="tr:TransmissionData/DocumentOfficialCode"/>
                          </xsl:when>
                          <xsl:otherwise>
                            Official
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="labelBoldLC" style="width: 145px;">Complete Code:</td>
                      <td class="labelLC" style="width: 200px;">
                        <xsl:choose>
                          <xsl:when test="tr:TransmissionData/DocumentCompleteCode != ''">
                            <xsl:value-of select="tr:TransmissionData/DocumentCompleteCode"/>
                          </xsl:when>
                          <xsl:otherwise>
                            Complete
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr><td class="labelLC" colspan="4" style="height: 10px;"></td></tr>
                    <tr>
                      <td colspan="4">
                        <table class="tableThirdLC" border="0" cellspacing="0" cellpadding="2" width="100%">
                          <tr>
                            <td class="heading2LC" style="width: 125px;"></td>
                            <td class="heading2LC" style="width: 200px;">Requesting</td>
                            <td class="heading2LC" style="width: 325px;">Fulfilling</td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Organization Group:</td>
                            <td class="labelLC">
                              <xsl:if test="tr:TransmissionData/Source/Organization/OPEID">OPEID: <xsl:value-of select="tr:TransmissionData/Source/Organization/OPEID"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/NCHELPID">NCHELPID: <xsl:value-of select="tr:TransmissionData/Source/Organization/NCHELPID"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/IPEDS">IPEDS: <xsl:value-of select="tr:TransmissionData/Source/Organization/IPEDS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/ATP">ATP: <xsl:value-of select="tr:TransmissionData/Source/Organization/ATP"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/FICE">FICE: <xsl:value-of select="tr:TransmissionData/Source/Organization/FICE"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/ACT">ACT: <xsl:value-of select="tr:TransmissionData/Source/Organization/ACT"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/CCD">CCD: <xsl:value-of select="tr:TransmissionData/Source/Organization/CCD"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/CEEBACT">CEEBACT: <xsl:value-of select="tr:TransmissionData/Source/Organization/CEEBACT"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/CSIS">CSIS: <xsl:value-of select="tr:TransmissionData/Source/Organization/CSIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/USIS">USIS: <xsl:value-of select="tr:TransmissionData/Source/Organization/USIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/ESIS">ESIS: <xsl:value-of select="tr:TransmissionData/Source/Organization/ESIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/DUNS">DUNS: <xsl:value-of select="tr:TransmissionData/Source/Organization/DUNS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Source/Organization/APAS">APAS: <xsl:value-of select="tr:TransmissionData/Source/Organization/APAS"/></xsl:if>
                            </td>
                            <td class="labelLC">
                              <xsl:if test="tr:TransmissionData/Destination/Organization/OPEID">OPEID: <xsl:value-of select="tr:TransmissionData/Destination/Organization/OPEID"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/NCHELPID">NCHELPID: <xsl:value-of select="tr:TransmissionData/Destination/Organization/NCHELPID"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/IPEDS">IPEDS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/IPEDS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/ATP">ATP: <xsl:value-of select="tr:TransmissionData/Destination/Organization/ATP"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/FICE">FICE: <xsl:value-of select="tr:TransmissionData/Destination/Organization/FICE"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/ACT">ACT: <xsl:value-of select="tr:TransmissionData/Destination/Organization/ACT"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/CCD">CCD: <xsl:value-of select="tr:TransmissionData/Destination/Organization/CCD"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/CEEBACT">CEEBACT: <xsl:value-of select="tr:TransmissionData/Destination/Organization/CEEBACT"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/CSIS">CSIS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/CSIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/USIS">USIS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/USIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/ESIS">ESIS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/ESIS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/DUNS">DUNS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/DUNS"/></xsl:if>
                              <xsl:if test="tr:TransmissionData/Destination/Organization/APAS">APAS: <xsl:value-of select="tr:TransmissionData/Destination/Organization/APAS"/></xsl:if>
                            </td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Organization ID:</td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Source/Organization/LocalOrganizationID/LocalOrganizationIDCode"/></td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Destination/Organization/LocalOrganizationID/LocalOrganizationIDCode"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">ID Qualifier:</td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Source/Organization/LocalOrganizationID/LocalOrganizationIDQualifier"/></td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Destination/Organization/LocalOrganizationID/LocalOrganizationIDQualifier"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Organization Name:</td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Source/Organization/OrganizationName"/></td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Destination/Organization/OrganizationName"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Phone:</td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Source/Organization/Contacts/Phone/PhoneNumber"/></td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Destination/Organization/Contacts/Phone/PhoneNumber"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Email:</td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Source/Organization/Contacts/Email/EmailAddress"/></td>
                            <td class="labelLC"><xsl:value-of select="tr:TransmissionData/Destination/Organization/Contacts/Email/EmailAddress"/></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
            </table>

            </xsl:for-each>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
