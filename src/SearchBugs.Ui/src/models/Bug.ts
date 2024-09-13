export class Bug {
  id: string;
  title: string;
  description: string;
  status: string;
  priority: string;
  severity: string;
  projectName: string;
  assignee: string;
  reporter: string;
  createdOnUtc: string;
  updatedOnUtc: string;
  constructor(
    id: string,
    title: string,
    description: string,
    status: string,
    priority: string,
    severity: string,
    projectName: string,
    assignee: string,
    reporter: string,
    createdOnUtc: string,
    updatedOnUtc: string
  ) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.status = status;
    this.priority = priority;
    this.severity = severity;
    this.projectName = projectName;
    this.assignee = assignee;
    this.reporter = reporter;
    this.createdOnUtc = createdOnUtc;
    this.updatedOnUtc = updatedOnUtc;
  }
}
