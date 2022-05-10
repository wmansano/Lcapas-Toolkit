<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >

  <xsl:template name="formatdate">
    <xsl:param name="DateTimeStr" />

    <xsl:if test="$DateTimeStr != ''" >
      <xsl:variable name="datestr">
        <xsl:value-of select="substring-before($DateTimeStr,'T')" />
      </xsl:variable>

      <xsl:variable name="MM">
        <xsl:value-of select="substring($datestr,6,2)" />
      </xsl:variable>

      <xsl:variable name="dd">
        <xsl:value-of select="substring($datestr,9,2)" />
      </xsl:variable>

      <xsl:variable name="yyyy">
        <xsl:value-of select="substring($datestr,1,4)" />
      </xsl:variable>

      <xsl:variable name="timestr">
        <xsl:value-of select="substring-after($DateTimeStr,'T')" />
      </xsl:variable>

      <xsl:variable name="HH">
        <xsl:value-of select="substring($timestr,1,2)" />
      </xsl:variable>

      <xsl:variable name="mm">
        <xsl:value-of select="substring($timestr,4,2)" />
      </xsl:variable>

      <xsl:variable name="ss">
        <xsl:value-of select="substring($timestr,7,2)" />
      </xsl:variable>

      <xsl:value-of select="concat($yyyy,'/',$MM,'/',$dd,' ',$HH,':',$mm,':',$ss)" />
    </xsl:if>
  </xsl:template>


  <xsl:template name="formatDateOnly">
    <xsl:param name="DateTimeStr" />

    <xsl:if test="$DateTimeStr != ''" >
      <xsl:variable name="datestr">
        <xsl:value-of select="substring-before($DateTimeStr,'T')" />
      </xsl:variable>

      <xsl:variable name="MM">
        <xsl:value-of select="substring($datestr,6,2)" />
      </xsl:variable>

      <xsl:variable name="dd">
        <xsl:value-of select="substring($datestr,9,2)" />
      </xsl:variable>

      <xsl:variable name="yyyy">
        <xsl:value-of select="substring($datestr,1,4)" />
      </xsl:variable>

      <xsl:value-of select="concat($yyyy,'/',$MM,'/',$dd)" />
    </xsl:if>
  </xsl:template>

  <xsl:template name="formatTimeOnly">
    <xsl:param name="DateTimeStr" />

    <xsl:if test="$DateTimeStr != ''" >
      <xsl:variable name="timestr">
        <xsl:value-of select="substring-after($DateTimeStr,'T')" />
      </xsl:variable>

      <xsl:variable name="HH">
        <xsl:value-of select="substring($timestr,1,2)" />
      </xsl:variable>

      <xsl:variable name="mm">
        <xsl:value-of select="substring($timestr,4,2)" />
      </xsl:variable>

      <xsl:variable name="ss">
        <xsl:value-of select="substring($timestr,7,2)" />
      </xsl:variable>

      <xsl:value-of select="concat($HH,':',$mm,':',$ss)" />
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
