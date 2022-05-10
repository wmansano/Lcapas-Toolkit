<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:AdmApp="urn:org:pesc:message:AdmissionsApplication:v1.3.0" exclude-result-prefixes="AdmApp">
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>Admissions Application (Post Secondary)</title>
        <style type="text/css">
          .container { margin-left: auto;  margin-right: auto;  width: 650px;  text-align: left;  }
          .main, .center { text-align: center;  padding: 5px;  }
          .left { float: left;  padding: 5px;  }
          .right { float: right;  padding: 5px;   }
          .top-right-fixed {  position: fixed; right: 15px; top: 15px;  }
          #stuNameLbl  { padding-left: 43%;  }
          .formTbl {  border: 1px solid gray; width: 100%; border-collapse: collapse; table-layout:fixed; }
          .formTbl td { vertical-align: top; overflow: hidden; }
          .instTbl { width: 100%;  border-collapse: collapse;  }
          .heading0 { font: 15px Arial;  padding: 5px;  }
          .heading1 { font: 12px Arial;  }
          .headerLbl { font: bold 12px Verdana;  background: lightgray; padding: 5px; }
          .formLbl { font: bold 11px Verdana; background: none; color: #666666; padding: 3px; }
          .contentData { font: 12px Arial; padding: 3px;  white-space: nowrap; min-height: 14px;  }
          .red { color:red; font:bold;}
          .highlight { background-color:yellow; font-weight: bold;}

          @media screen {.page-break { margin: 15px 0 20px; border-bottom: 1px dashed #8c8b8b; }}

          @media print {
          .page-break { page-break-before: always; margin-bottom: 20px; }
          .header { display: table-header-group; position: fixed; top: 0; width: 100%; }
          .spacer { height: 20px; }
          .formTbl {  width: 650px; }
          .content { display: table-row-group; }
          .footer { display: table-footer-group; position: fixed; bottom: 0; width: 100%; }
          .hide-print { display: none; }
          }
        </style>
        <script type="text/javascript">
          function setroot() {
            var rootpath = window.location.protocol + "//" + window.location.host + "/";
            if (window.location.hostname != "localhost")
            {
              var path = window.location.pathname;
              if (path.indexOf("/") == 0) { path = path.substring(1); }
              path = path.split("/", 1);
              if (path != "") { rootpath = rootpath + path + "/lcapasadmin/"; }
            }
              <!--document.getElementById('print_img').src = rootpath + "Content/Images/Print.png";-->
          }
        </script>
      </head>
      <body onload="javascript:setroot();">
        <div class="main">
          <!--<div class="hide-print right top-right-fixed">
            <a href="javascript:window.print()">
              <img id="print_img" src="../Content/Images/Print.png" title="Print Document" />
            </a>
          </div>-->
          <div class="container">

            <xsl:for-each select="ArrayOfApplicationObj/ApplicationObj">
              <xsl:choose>
                <xsl:when test="position() != 1">
                  <div class="page-break">
                    <br/>
                  </div>
                </xsl:when>
                <xsl:otherwise>
                  <div class="spacer">
                    <br/>
                  </div>
                </xsl:otherwise>
              </xsl:choose>         

              <div class="content">
                <div class="page">
                  <div class="header">
                    <div class="heading0 center">APAS IMPORT-DATA</div>
                  </div>
                  <div class="heading1 left">DATA SHEET</div>
                  <div class="heading1 right red">
                    <xsl:value-of select="./DateCancelled"/>
                  </div>

                  <table class="formTbl" border="1">

                    <!-- Personal Information -->
                    <tr class="headerRow">
                      <td colspan="6">
                        <div class="headerLbl">PERSONAL INFORMATION:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="FullNameLbl" class="formLbl">NAME (Last, First Middle):</div>
                        <div class="contentData">
                          <xsl:value-of select="LastNameDesc"/>
                          <xsl:if test="./FirstNameDesc != ''">
                            <xsl:text xml:space="preserve">, </xsl:text>
                            <xsl:value-of select="FirstNameDesc"/>&#160;
                          </xsl:if>
                          <xsl:value-of select="MiddleNameDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="LCIDLbl" class="formLbl">LC ID:</div>
                        <div class="contentData">
                          <xsl:value-of select="PrevSNumber"/>
                        </div>
                      </td>
                    </tr>
                    <tr class="headerRow">
                      <td colspan="6">
                        <div id="FormerNamesLbl" class="formLbl">FORMER NAME (Last, First Middle):</div>
                        <div class="contentData">
                          <xsl:value-of select="FormerLastName"/>
                          <xsl:if test="./FormerFirstName != ''">
                            <xsl:text xml:space="preserve">, </xsl:text>
                            <xsl:value-of select="FormerFirstName"/>&#160;
                          </xsl:if>
                          <xsl:value-of select="FormerMiddleName"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="AddrLine1Lbl" class="formLbl">ADDR LN1:</div>
                        <div class="contentData">
                          <xsl:value-of select="AddressLine1Desc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="AddrLine2Lbl" class="formLbl">ADDR LN 2:</div>
                        <div class="contentData">
                          <xsl:value-of select="AddressLine2Desc"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="CityProvPostalLbl" class="formLbl">CITY, PROV, POSTAL:</div>
                        <div class="contentData">
                          <xsl:value-of select="AddressCityDesc"/>
                          <xsl:if test="./AddressState != ''">
                            <xsl:text xml:space="preserve">, </xsl:text>
                            <xsl:value-of select="AddressState"/>
                          </xsl:if>
                          <xsl:if test="./AddressPostalCode != ''">
                            <xsl:text xml:space="preserve">, </xsl:text>
                            <xsl:value-of select="AddressPostalCode"/>
                          </xsl:if>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="CountryLbl" class="formLbl">COUNTRY:</div>
                        <div class="contentData">
							<xsl:if test="string-length(AddressCountryDesc) &lt;= 0">
								<xsl:attribute name="class">contentData highlight</xsl:attribute>
							</xsl:if>
							<xsl:value-of select="AddressCountryDesc"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        <div id="PhoneLbl" class="formLbl">PERMANENT PHONE:</div>
                        <div class="contentData">
                          <xsl:if test="string-length(PermanentPhoneNumber) > 12">
                            <xsl:attribute name="class">contentData highlight</xsl:attribute>
                          </xsl:if>
                          <xsl:value-of select="PermanentPhoneNumber"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="PhoneLbl" class="formLbl">DAY PHONE:</div>
                        <div class="contentData">
                          <xsl:if test="string-length(DayPhoneNumber) > 12">
                            <xsl:attribute name="class">contentData highlight</xsl:attribute>
                          </xsl:if>
                          <xsl:value-of select="DayPhoneNumber"/>
                          <xsl:if test="./DayPhoneExtensionNumber != ''">
                            <xsl:text xml:space="preserve">, </xsl:text>
                            <xsl:value-of select="DayPhoneExtensionNumber"/>
                          </xsl:if>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="MobileLbl" class="formLbl">MOBILE PHONE:</div>
                        <div class="contentData">
                          <xsl:if test="string-length(MobilePhoneNumber) > 12">
                            <xsl:attribute name="class">contentData highlight</xsl:attribute>
                          </xsl:if>
                          <xsl:value-of select="MobilePhoneNumber"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="GenderLbl" class="formLbl">GENDER:</div>
                        <div class="contentData">
                          <xsl:value-of select="GenderDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="BirthLbl" class="formLbl">BIRTHDATE (yyyy/MM/dd):</div>
                        <div class="contentData">
                          <xsl:value-of select="BirthDateView"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="AborStatLbl" class="formLbl">ABORIGINAL STATUS:</div>
                        <div class="contentData">
                          <xsl:value-of select="RaceEthnicityDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="EmailLbl" class="formLbl">EMAIL:</div>
                        <div class="contentData">
                          <xsl:value-of select="Email"/>
                        </div>
                      </td>
                    </tr>

                    <!-- Application Information -->
                    <tr class="headerRow">
                      <td colspan="6">
                        <div id="AppInfoLbl" class="headerLbl">APPLICATION INFORMATION:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        <div id="TermLbl" class="formLbl">TERM:</div>
                        <div class="contentData">
                          <xsl:value-of select="Term"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="StuTypeLbl" class="formLbl">TYPE OF STUDENT:</div>
                        <div class="contentData">
                          <xsl:value-of select="StudyLoadDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="PrevApplyLbl" class="formLbl">PREVIOUSLY APPLIED:</div>
                        <div class="contentData">
                          <xsl:value-of select="PreviouslyApplied"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="ProgramLbl" class="formLbl">PROGRAM:</div>
                        <div class="contentData">
                          <xsl:value-of select="Program"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="LocationLbl" class="formLbl">LOCATION:</div>
                        <div class="contentData">
                          <xsl:value-of select="ProgramLocation"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="AdmitStatLbl" class="formLbl">ADMIT STATUS:</div>
                        <div class="contentData">
                          <xsl:value-of select="AdmitStatus"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="FamilyAttendedCollegeLbl" class="formLbl">FAMILY ATTENDED COLLEGE:</div>
                        <div class="contentData">
                          <xsl:value-of select="FamilyAttendedCollege"/>
                        </div>
                      </td>
                      <!--<td colspan="2">
                        <div id="ResidencyLbl" class="formLbl">RESIDENCY STATUS:</div>
                        <div class="contentData">
                          <xsl:value-of select="ResidencyStatusDesc"/>
                        </div>
                      </td>-->
                    </tr>

                    <!-- Citizenship Information -->
                    <tr class="headerRow">
                      <td colspan="6">
                        <div id="AppInfoLbl" class="headerLbl">CITIZENSHIP INFORMATION:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        <div id="CitizenLbl" class="formLbl">CITIZENSHIP STATUS:</div>
                        <div class="contentData">
                          <xsl:value-of select="CitizenshipStatusDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="CountryLbl" class="formLbl">CITIZENSHIP COUNTRY:</div>
                        <div class="contentData">
                          <xsl:value-of select="CitizenshipCountryDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="FirstLangLbl" class="formLbl">FIRST LANGUAGE:</div>
                        <div class="contentData">
                          <xsl:value-of select="FirstLanguage"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="EntryDateLbl" class="formLbl">ENTRY DATE (yyyy/MM/dd):</div>
                        <div class="contentData">
                          <xsl:value-of select="ImmigrationEntryDateView"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="ResCountryLbl" class="formLbl">RESIDENCY COUNTRY:</div>
                        <div class="contentData">
                          <xsl:value-of select="ResidencyCountryDesc"/>
                        </div>
                      </td>
                    </tr>

                    <!-- Other Information -->
                    <tr class="headerRow">
                      <td colspan="6">
                        <div id="OtherInfoLbl" class="headerLbl">OTHER INFORMATION:</div>
                      </td>
                    </tr>
                    <tr>
                      <!--<td colspan="2">
                        <div id="MarStatLbl" class="formLbl">MARITAL STATUS:</div>
                        <div class="contentData">
                          <xsl:value-of select="MaritalStatusDesc"/>
                        </div>
                      </td>-->
                      <td colspan="4">
                        <div id="AbEdIdLbl" class="formLbl">U of L ID:</div>
                        <div class="contentData"></div>
                      </td>
                      <td colspan="2">
                        <div id="AbEdIdLbl" class="formLbl">ASN:</div>
                        <div class="contentData">
                          <xsl:value-of select="ASN"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="AltContactLbl" class="formLbl">ALTERNATE CONTACT:</div>
                        <div class="contentData">
                          <xsl:value-of select="EmergencyContactName"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="AltContPhLbl" class="formLbl">ALT CONTACT PH:</div>
                        <div class="contentData">
                          <xsl:value-of select="EmergencyContactPhone"/>
                        </div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4">
                        <div id="InflToApplyLbl" class="formLbl">INFLUENCED TO APPLY:</div>
                        <div class="contentData">
                          <xsl:value-of select="FirstLearnedDesc"/>
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="DisabilityLbl" class="formLbl">DISABILITY:</div>
                        <div class="contentData">
                          <xsl:value-of select="Disability"/>
                        </div>
                      </td>
                    </tr>

                    <!-- Database Information -->
                    <tr class="headerRow">
                      <td colspan="6">
                        <div id="OtherInfoLbl" class="headerLbl">DATABASE USE ONLY:</div>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        <div id="SubmittedLbl" class="formLbl">SUBMITTED:</div>
                        <div class="contentData">
                          <!--<xsl:value-of select="DateSubmitted"/>-->
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="ExportedLbl" class="formLbl">EXPORTED:</div>
                        <div class="contentData">
                          <!--<xsl:value-of select="DateExported"/>-->
                        </div>
                      </td>
                      <td colspan="2">
                        <div id="AbEdIdLbl" class="formLbl">APPLICATION ID:</div>
                        <div class="contentData">
                          <xsl:value-of select="ApplicationID"/>
                        </div>
                      </td>
                    </tr>
                  </table>
                </div>
                
                <div class="page-break">
                  <br/>
                </div>

                <div class="page">
                  <div class="header">
                    <div class="heading0 center">APAS IMPORT-DATA</div>
                  </div>
                  <!--<div id="EduHistLbl" class="heading1 left">EDUCATION HISTORY</div>-->
                  <!--<div id="DateLbl" class="heading1 right">
                    <xsl:value-of select="currentDate"/>
                  </div>
                  <div id="stuNameLbl" class="heading1">
                    <xsl:value-of select="LastName"/>
                    <xsl:if test="./FirstName != ''">
                      <xsl:text xml:space="preserve">, </xsl:text>
                      <xsl:value-of select="FirstName"/>&#160;
                    </xsl:if>
                    <xsl:value-of select="MiddleName"/>
                  </div>-->
                  <table class="formTbl" border="0">
                    <!-- Institutions Information -->
                    <tr class="headerRow">
                      <td colspan="3">
                        <div id="InstLbl" class="headerLbl">Institution:</div>
                      </td>
                      <td>
                        <div id="InstIdLbl" class="headerLbl">Inst. ID:</div>
                      </td>
                      <td>
                        <div id="LocationLbl" class="headerLbl">Location:</div>
                      </td>
                      <td>
                        <div id="DurationLbl" class="headerLbl">Duration:</div>
                      </td>
                    </tr>
                    <xsl:for-each select="ApplicationInstitutions/ApplInstObj">
                      <tr>
                        <td colspan="3">
                          <div class="contentData">
                            <xsl:value-of select="InstitutionName"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="LocalOrganizationID"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="CityProv"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="Duration"/>
                          </div>
                        </td>
                      </tr>
                    </xsl:for-each>
                  </table>
                  <br />
                  <br />
                  <table class="formTbl" border="0">
                    <!-- Courses -->
                    <tr class="headerRow">
                      <td colspan="3">
                        <div id="CrsLbl" class="headerLbl">Course:</div>
                      </td>
                      <td colspan="2">
                        <div id="CrsIdLbl" class="headerLbl">Course ID:</div>
                      </td>
                      <td>
                        <div id="GradeLbl" class="headerLbl">Grade:</div>
                      </td>
                      <td>
                        <div id="CreditLbl" class="headerLbl">Credit:</div>
                      </td>
                      <td>
                        <div id="StatusLbl" class="headerLbl">Status:</div>
                      </td>
                      <td>
                        <div id="EndDateLbl" class="headerLbl">EndDate:</div>
                      </td>
                    </tr>
                    <xsl:for-each select="ApplicationCourses/ApplCrsObj">
                      <tr>
                        <td colspan="3">
                          <div class="contentData">
                            <xsl:value-of select="CourseName"/>
                          </div>
                        </td>
                        <td colspan="2">
                          <div class="contentData">
                            <xsl:value-of select="CourseId"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="Grade"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="Credit"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="Status"/>
                          </div>
                        </td>
                        <td>
                          <div class="contentData">
                            <xsl:value-of select="EndDate"/>
                          </div>
                        </td>
                      </tr>
                    </xsl:for-each>
                  </table>
                </div>
              </div>

              <!--<div class="footer">
                <div id="RegistrarLbl" class="heading1 left">Registrar's Office</div>
                <div id="footerDateLbl" class="heading1 right">
                  <xsl:value-of select="currentDate"/>
                </div>
                <div id="foorterLbl" class="heading1 center">Lethbridge College</div>
              </div>-->

            </xsl:for-each>

          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
