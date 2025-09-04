import { Routes } from '@angular/router';
import { DatasetPageComponent } from './page-components/dataset/dataset-page/dataset-page.component';
import { DashboardComponent } from './page-components/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'datasets', component: DatasetPageComponent },
];
