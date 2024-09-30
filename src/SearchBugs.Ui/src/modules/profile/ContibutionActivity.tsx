import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
  } from "@/components/ui/card";
  import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
  } from "@/components/ui/select";

const ContributionCell: React.FC<{ level: number }> = ({ level }) => {
    const bgColor = [
      "bg-gray-100",
      "bg-green-100",
      "bg-green-300",
      "bg-green-500",
      "bg-green-700",
    ][level];
  
    return <div className={`w-3 h-3 ${bgColor} rounded-sm`} />;
  };

const ContributionGraph: React.FC = () => {
    const weeks = 52;
    const days = 7;
  
    const generateRandomContributions = () => {
      return Array.from({ length: weeks * days }, () =>
        Math.floor(Math.random() * 5)
      );
    };
  
    const contributions = generateRandomContributions();
  
    return (
      <div className="flex flex-no-wrap gap-1 w-full overflow-x-auto">
        {Array.from({ length: weeks }).map((_, weekIndex) => (
          <div key={weekIndex} className="flex flex-col gap-1">
            {Array.from({ length: days }).map((_, dayIndex) => (
              <ContributionCell
                key={dayIndex}
                level={contributions[weekIndex * days + dayIndex]}
              />
            ))}
          </div>
        ))}
      </div>
    );
  };

interface ContributionActivityProps {
    years: string[];
    selectedYear: string;
    setSelectedYear: (year: string) => void;
}


export const ContributionActivity: React.FC<ContributionActivityProps> = ({
    years,
    selectedYear,
    setSelectedYear,
  }) => {

  return (
    <Card>
      <CardHeader className="flex flex-row items-center justify-between py-3">
        <CardTitle className="text-lg">Contribution Activity</CardTitle>
        <Select value={selectedYear} onValueChange={setSelectedYear}>
          <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Select year" />
          </SelectTrigger>
          <SelectContent>
            {years.map((year) => (
              <SelectItem key={year} value={year}>
                {year}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </CardHeader>
      <CardContent>
        <div className="mb-2 flex items-center justify-start space-x-2 text-sm text-gray-500">
          <span>Less</span>
          <ContributionCell level={0} />
          <ContributionCell level={1} />
          <ContributionCell level={2} />
          <ContributionCell level={3} />
          <ContributionCell level={4} />
          <span>More</span>
        </div>
        <ContributionGraph />
      </CardContent>
    </Card>
  );
};
