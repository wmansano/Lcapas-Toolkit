<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ColTrn="urn:org:pesc:message:CollegeTranscript:v1.6.0" exclude-result-prefixes="ColTrn">
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="Functions.xsl"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Transcript (Post Secondary)</title>
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
          .tableSecondLC td.cellLC, tableThirdLC td.cellLC { font: 12px Arial; border: 0px; border-bottom: 1px solid #ebebeb; }

          .tableMainLC .heading0LC { font: bold 20px Arial; background: none; border-bottom: 1px solid #999966; padding: 5px; }
          .tableMainLC td.heading1LC { font: bold 14px Verdana; background: none; border-bottom: 1px solid #bbbb88; color: #777700; }
          .tableSecondLC td.heading2LC { font :bold 12px Verdana; border-bottom: 1px solid #bbbbbb; background: none; color: #666666; }
          .tableSecondLC td.heading3LC { font: bold 12px Verdana; border-bottom: 1px solid #bbbbbb; background: #ffffff; color: #444444;}
        </style>
      </head>
      <body>
        <div class="mainLC">
          <div class="containerLC">
            <xsl:for-each select=".//ColTrn:CollegeTranscript">

              <table class="tableMainLC" cellpadding="0" cellspacing="0" border="0">
              <tr>
                <td>
                  <p class="heading0LC" align="center">Transcript (Post Secondary)</p>
                  <xsl:if test="TransmissionData/DocumentProcessCode = 'TEST'">
                    <div class="labelRedLC" align="center">
                      <xsl:value-of select="TransmissionData/DocumentProcessCode"/>
                    </div>
                  </xsl:if>
                </td>
              </tr>
              <tr><td style="height: 20px;"></td></tr>
              
              <!-- Student Header -->
              <tr><td class="heading1LC">Student Information</td></tr>
              <tr>
                <td>
                  <table class="tableSecondLC" cellpadding="2" cellspacing="0">
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Agency Assigned ID:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="Student/Person/AgencyIdentifier/AgencyAssignedID"/></td>
                      <td class="labelBoldLC" style="width: 145px;">School Assigned ID</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="Student/Person/SchoolAssignedPersonID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Recipient Assigned ID:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/RecipientAssignedID"/></td>
                      <td class="labelBoldLC">Last High School:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/HighSchool/OrganizationName"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Gender:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/Gender/GenderCode"/></td>
                      <td class="labelBoldLC">Birthdate:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/Birth/BirthDate"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">First Name:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/Name/FirstName"/></td>
                      <td class="labelBoldLC">Last Name:</td>
                      <td class="labelLC"><xsl:value-of select="Student/Person/Name/LastName"/></td>
                    </tr>
                    <xsl:if test="Student/Person/Name/MiddleName">
                      <tr>
                        <td class="labelBoldLC">Middle Name(s):</td>
                        <td class="labelLC" colspan="3">
                          <xsl:for-each select="Student/Person/Name/MiddleName">
                            <xsl:value-of select="."/> 
                          </xsl:for-each>
                        </td>
                      </tr>
                    </xsl:if>
                    
                    <!-- Alternate Names -->
                    <xsl:if test="Student/Person/AlternateName">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <tr><td colspan="4" class="heading2LC">Former Names</td></tr>
                      <xsl:for-each select="Student/Person/AlternateName">
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
                    <xsl:if test="Student/Person/Contacts/Address">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="Student/Person/Contacts/Address">
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
                    <xsl:if test="Student/Person/Contacts/Phone">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="Student/Person/Contacts/Phone">
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
                    <xsl:if test="Student/Person/Contacts/Email">
                      <tr><td colspan="4" style="height: 15px;"></td></tr>
                      <xsl:for-each select="Student/Person/Contacts/Email">
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
              
              <!-- Academic Records -->
              <tr><td colspan="4" class="heading1LC">Academic Record</td></tr>
              <tr>
                <td>
                  <xsl:for-each select="Student/AcademicRecord/AcademicSession">
                    <p/>
                    <table class="tableSecondLC" cellpadding="2" cellspacing="0">
                      <tr><td class="heading3LC" colspan="4">Academic Session</td></tr>
                      <tr>
                        <td class="labelBoldLC" style="width: 125px;">Designator</td>
                        <td class="labelLC" style="width: 200px;"><xsl:value-of select="AcademicSessionDetail/SessionDesignator"/></td>
                        <td class="labelBoldLC" style="width: 125px;">Name</td>
                        <td class="labelLC" style="width: 200px;"><xsl:value-of select="AcademicSessionDetail/SessionName"/></td>
                      </tr>
                      <tr>
                        <td class="labelBoldLC">Type</td>
                        <td class="labelLC"><xsl:value-of select="AcademicSessionDetail/SessionType"/></td>
                        <td class="labelBoldLC">Dates</td>
                        <td class="labelLC"><xsl:value-of select="AcademicSessionDetail/SessionBeginDate"/> - <xsl:value-of select="AcademicSessionDetail/SessionEndDate"/></td>
                      </tr>
                      <tr><td colspan="4" style="height: 20px;"></td></tr>
                      
                      <!-- Academic Program -->
                      <xsl:if test="AcademicProgram">
                        <tr>
                          <td class="heading2LC" colspan="1">Program Type</td>
                          <td class="heading2LC" colspan="3">Program Name</td>
                        </tr>
                        <xsl:for-each select="AcademicProgram">
                          <tr>
                            <td class="cellLC" colspan="1"><xsl:value-of select="AcademicProgramType"/></td>
                            <td class="cellLC" colspan="3"><xsl:value-of select="AcademicProgramName"/></td>
                          </tr>
                        </xsl:for-each>
                        <tr><td colspan="4" style="height: 20px;"></td></tr>
                      </xsl:if>
                      
                      <tr>
                        <td colspan="4">
                          <table class="tableThirdLC" border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                              <td class="heading2LC" style="width: 100px;">Course</td>
                              <td class="heading2LC" style="width: 250px;">Course Title</td>
                              <td class="heading2LC" style="width: 100px;">Credit Basis</td>
                              <td class="heading2LC" style="width: 100px;">Credit level</td>
                              <td class="heading2LC" style="width: 100px;">Grade</td>
                            </tr>
                            <xsl:for-each select="Course">
                              <tr>
                                <td class="cellLC"><xsl:value-of select="CourseSubjectAbbreviation"/> <xsl:value-of select="CourseNumber"/></td>
                                <td class="cellLC"><xsl:value-of select="CourseTitle"/></td>
                                <td class="cellLC"><xsl:value-of select="CourseCreditBasis"/></td>
                                <td class="cellLC"><xsl:value-of select="CourseCreditLevel"/></td>
                                <td class="cellLC"><xsl:value-of select="CourseAcademicGrade"/></td>
                              </tr>
                            </xsl:for-each>
                          </table>
                        </td>
                      </tr>
                      <tr><td colspan="4" style="height: 20px;"></td></tr>
                      
                      <!-- GPA -->
                      <xsl:if test="AcademicSummary/GPA">
                        <tr><td class="heading2LC" colspan="4">GPA</td></tr>
                        <xsl:for-each select="AcademicSummary/GPA">
                          <tr>
                            <td class="labelBoldLC">Credit Hours Earned</td>
                            <td class="labelLC"><xsl:value-of select="CreditHoursEarned"/></td>
                            <td class="labelBoldLC">Grade Point Average</td>
                            <td class="labelLC"><xsl:value-of select="GradePointAverage"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Total Quality Points</td>
                            <td class="labelLC"><xsl:value-of select="TotalQualityPoints"/></td>
                            <td class="labelBoldLC">Credit Hours for GPA</td>
                            <td class="labelLC"><xsl:value-of select="CreditHoursforGPA"/></td>
                          </tr>
                        </xsl:for-each>
                      </xsl:if>
                    </table>
                  </xsl:for-each>
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
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="TransmissionData/DocumentTypeCode"/></td>
                      <td class="labelBoldLC" style="width: 125px;">Document ID:</td>
                      <td class="labelLC" style="width: 200px;"><xsl:value-of select="TransmissionData/DocumentID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC">Created Date:</td>
                      <td class="labelLC">
                        <xsl:call-template name="formatdate">
                          <xsl:with-param name="DateTimeStr" select="TransmissionData/CreatedDateTime"/>
                        </xsl:call-template>
                      </td>
                      <td class="labelBoldLC">Request Tracking ID:</td>
                      <td class="labelLC"><xsl:value-of select="TransmissionData/RequestTrackingID"/></td>
                    </tr>
                    <tr>
                      <td class="labelBoldLC" style="width: 125px;">Transmission Type:</td>
                      <td class="labelLC" style="width: 180px;"><xsl:value-of select="TransmissionData/TransmissionType"/></td>
                      <td class="labelBoldLC" style="width: 145px;">Process Code:</td>
                      <td class="labelLC" style="width: 200px;">
                        <xsl:choose>
                          <xsl:when test="TransmissionData/DocumentProcessCode != ''">
                            <xsl:value-of select="TransmissionData/DocumentProcessCode"/>
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
                          <xsl:when test="TransmissionData/DocumentOfficialCode != ''">
                            <xsl:value-of select="TransmissionData/DocumentOfficialCode"/>
                          </xsl:when>
                          <xsl:otherwise>
                            Official
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                      <td class="labelBoldLC" style="width: 145px;">Complete Code:</td>
                      <td class="labelLC" style="width: 200px;">
                        <xsl:choose>
                          <xsl:when test="TransmissionData/DocumentCompleteCode != ''">
                            <xsl:value-of select="TransmissionData/DocumentCompleteCode"/>
                          </xsl:when>
                          <xsl:otherwise>
                            Complete
                          </xsl:otherwise>
                        </xsl:choose>
                      </td>
                    </tr>
                    <tr><td colspan="4" style="height: 10px;"></td></tr>
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
                              <xsl:if test="TransmissionData/Source/Organization/OPEID">OPEID: <xsl:value-of select="TransmissionData/Source/Organization/OPEID"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/NCHELPID">NCHELPID: <xsl:value-of select="TransmissionData/Source/Organization/NCHELPID"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/IPEDS">IPEDS: <xsl:value-of select="TransmissionData/Source/Organization/IPEDS"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/ATP">ATP: <xsl:value-of select="TransmissionData/Source/Organization/ATP"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/FICE">FICE: <xsl:value-of select="TransmissionData/Source/Organization/FICE"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/ACT">ACT: <xsl:value-of select="TransmissionData/Source/Organization/ACT"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/CCD">CCD: <xsl:value-of select="TransmissionData/Source/Organization/CCD"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/CEEBACT">CEEBACT: <xsl:value-of select="TransmissionData/Source/Organization/CEEBACT"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/CSIS">CSIS: <xsl:value-of select="TransmissionData/Source/Organization/CSIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/USIS">USIS: <xsl:value-of select="TransmissionData/Source/Organization/USIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/ESIS">ESIS: <xsl:value-of select="TransmissionData/Source/Organization/ESIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/DUNS">DUNS: <xsl:value-of select="TransmissionData/Source/Organization/DUNS"/></xsl:if>
                              <xsl:if test="TransmissionData/Source/Organization/APAS">APAS: <xsl:value-of select="TransmissionData/Source/Organization/APAS"/></xsl:if>
                            </td>
                            <td class="labelLC">
                              <xsl:if test="TransmissionData/Destination/Organization/OPEID">OPEID: <xsl:value-of select="TransmissionData/Destination/Organization/OPEID"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/NCHELPID">NCHELPID: <xsl:value-of select="TransmissionData/Destination/Organization/NCHELPID"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/IPEDS">IPEDS: <xsl:value-of select="TransmissionData/Destination/Organization/IPEDS"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/ATP">ATP: <xsl:value-of select="TransmissionData/Destination/Organization/ATP"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/FICE">FICE: <xsl:value-of select="TransmissionData/Destination/Organization/FICE"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/ACT">ACT: <xsl:value-of select="TransmissionData/Destination/Organization/ACT"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/CCD">CCD: <xsl:value-of select="TransmissionData/Destination/Organization/CCD"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/CEEBACT">CEEBACT: <xsl:value-of select="TransmissionData/Destination/Organization/CEEBACT"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/CSIS">CSIS: <xsl:value-of select="TransmissionData/Destination/Organization/CSIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/USIS">USIS: <xsl:value-of select="TransmissionData/Destination/Organization/USIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/ESIS">ESIS: <xsl:value-of select="TransmissionData/Destination/Organization/ESIS"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/DUNS">DUNS: <xsl:value-of select="TransmissionData/Destination/Organization/DUNS"/></xsl:if>
                              <xsl:if test="TransmissionData/Destination/Organization/APAS">APAS: <xsl:value-of select="TransmissionData/Destination/Organization/APAS"/></xsl:if>
                            </td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Organization ID:</td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Source/Organization/LocalOrganizationID/LocalOrganizationIDCode"/></td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Destination/Organization/LocalOrganizationID/LocalOrganizationIDCode"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">ID Qualifier:</td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Source/Organization/LocalOrganizationID/LocalOrganizationIDQualifier"/></td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Destination/Organization/LocalOrganizationID/LocalOrganizationIDQualifier"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Organization Name:</td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Source/Organization/OrganizationName"/></td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Destination/Organization/OrganizationName"/></td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Phone:</td>
                            <td class="labelLC">
                              <xsl:value-of select="TransmissionData/Source/Organization/Contacts/Phone/AreaCityCode"/>&#160;
                              <xsl:value-of select="TransmissionData/Source/Organization/Contacts/Phone/PhoneNumber"/>
                            </td>
                            <td class="labelLC">
                              <xsl:value-of select="TransmissionData/Destination/Organization/Contacts/Phone/AreaCityCode"/>&#160;
                              <xsl:value-of select="TransmissionData/Destination/Organization/Contacts/Phone/PhoneNumber"/>
                            </td>
                          </tr>
                          <tr>
                            <td class="labelBoldLC">Email:</td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Source/Organization/Contacts/Email/EmailAddress"/></td>
                            <td class="labelLC"><xsl:value-of select="TransmissionData/Destination/Organization/Contacts/Email/EmailAddress"/></td>
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
