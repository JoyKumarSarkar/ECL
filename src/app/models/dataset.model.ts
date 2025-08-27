export interface DatasetListResponse {
  datasetId: number;
  datasetName: string;
  source: string;
  cutoff: string;
  isRelated: boolean;
  fileCount: number;
  createdBy: string;
  createdOn: string;
}
