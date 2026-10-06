import React from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { Clock, Globe, MapPin } from "lucide-react";
import { useTimezone } from "@/hooks/useTimezone";
import { POPULAR_TIMEZONES } from "../../stores/global/timezoneStore";

interface TimezoneSettingsProps {
  className?: string;
}

export const TimezoneSettings: React.FC<TimezoneSettingsProps> = ({
  className,
}) => {
  const {
    timezone,
    detectedTimezone,
    useAutoDetection,
    currentTimezone,
    setTimezone,
    setUseAutoDetection,
    getTimezoneDisplay,
    getCurrentTime,
  } = useTimezone();

  const handleTimezoneChange = (newTimezone: string) => {
    setTimezone(newTimezone);
  };

  const handleAutoDetectionToggle = () => {
    setUseAutoDetection(!useAutoDetection);
  };

  return (
    <Card className={className}>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Globe className="h-5 w-5" />
          Timezone Settings
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-6">
        {/* Current Status */}
        <div className="space-y-3">
          <div className="flex items-center gap-2">
            <Clock className="h-4 w-4 text-muted-foreground" />
            <Label className="text-sm font-medium">Current Time</Label>
          </div>
          <div className="pl-6">
            <p className="text-lg font-mono">{getCurrentTime()}</p>
            <p className="text-sm text-muted-foreground mt-1">
              {getTimezoneDisplay(currentTimezone)}
            </p>
          </div>
        </div>

        {/* Auto-detection */}
        <div className="space-y-3">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-2">
              <MapPin className="h-4 w-4 text-muted-foreground" />
              <Label className="text-sm font-medium">
                Auto-detect timezone
              </Label>
            </div>
            <Button
              variant={useAutoDetection ? "default" : "outline-solid"}
              size="sm"
              onClick={handleAutoDetectionToggle}
            >
              {useAutoDetection ? "Enabled" : "Disabled"}
            </Button>
          </div>

          {useAutoDetection && (
            <div className="pl-6">
              <Badge variant="secondary" className="text-xs">
                Auto-detected: {getTimezoneDisplay(detectedTimezone)}
              </Badge>
            </div>
          )}
        </div>

        {/* Manual Selection */}
        {!useAutoDetection && (
          <div className="space-y-3">
            <Label className="text-sm font-medium">Select Timezone</Label>
            <Select value={timezone} onValueChange={handleTimezoneChange}>
              <SelectTrigger>
                <SelectValue placeholder="Choose your timezone" />
              </SelectTrigger>
              <SelectContent>
                {POPULAR_TIMEZONES.map((tz) => (
                  <SelectItem key={tz.value} value={tz.value}>
                    {tz.label}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
        )}

        {/* Help Text */}
        <div className="text-xs text-muted-foreground">
          <p>
            Your timezone setting affects how dates and times are displayed
            throughout the application.
            {useAutoDetection
              ? " Auto-detection uses your browser's timezone setting."
              : " You can manually select your preferred timezone above."}
          </p>
        </div>
      </CardContent>
    </Card>
  );
};
