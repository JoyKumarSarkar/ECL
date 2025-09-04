import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { DatasetService } from '../../../service/dataset.service';
import { CommonModule } from '@angular/common';
import { DatasetListResponse } from '../../../models/dataset.model';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';


@Component({
  selector: 'app-dataset-page',
  standalone: true,
  imports: [
    CommonModule,
    MatProgressBarModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule
  ],
  templateUrl: './dataset-page.component.html',
  styleUrl: './dataset-page.component.css'
})

export class DatasetPageComponent implements OnInit, AfterViewInit {

  MatProgressBar: boolean = false;

  Response: DatasetListResponse[] = [];

  DisplayedColumns: string[] = ['datasetName', 'source', 'fileCount', 'cutOff', 'related', 'createdBy', 'createdOn', 'actions'];
  DataSource!: MatTableDataSource<DatasetListResponse>;

  @ViewChild(MatPaginator) Paginator!: MatPaginator;
  @ViewChild(MatSort) Sort!: MatSort;

  constructor(private dataset: DatasetService) { }

  ngOnInit(): void {
    this.MatProgressBar = true;
    this.dataset.GetDatasetList().subscribe({
      next: (response: any) => {
        if (response && response.isSuccess === true) {
          this.Response = response.datasetList as DatasetListResponse[];
          this.DataSource = new MatTableDataSource(this.Response);
          this.DataSource.paginator = this.Paginator;
          this.DataSource.sort = this.Sort;
          // console.log(this.Response);
          this.MatProgressBar = false;
        }
      },
      error: error => {
        console.error('Error fetching menu details:', error);
      },
      complete: () => {
        this.MatProgressBar = false;
        console.log('Menu fetch operation completed');
      }
    })
  }

  ngAfterViewInit() {
    this.DataSource.paginator = this.Paginator;
    this.DataSource.sort = this.Sort;
  }

  ApplyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.DataSource.filter = filterValue.trim().toLowerCase();

    if (this.DataSource.paginator) {
      this.DataSource.paginator.firstPage();
    }
  }

}
