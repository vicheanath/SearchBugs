export class Project {
  id: string;
  name: string;
  description: string;
  createdOnUtc: string;
  updatedOnUtc: string;

  constructor(
    id: string,
    name: string,
    description: string,
    createdOnUtc: string,
    updatedOnUtc: string
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.createdOnUtc = createdOnUtc;
    this.updatedOnUtc = updatedOnUtc;
  }
}
