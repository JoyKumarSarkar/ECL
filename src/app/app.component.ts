import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MenuDetails, Return } from './models/home.model';
import { HomeService } from './service/home.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    CommonModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  constructor(private home: HomeService) { }

  AllMenuDetails: MenuDetails[] = [];
  OpenParentIndex: number | null = null;

  ngOnInit(): void {
    this.home.GetMenu().subscribe({
      next: (response: Return<MenuDetails[]>) => {
        if (response && response.success === true) {
          this.AllMenuDetails = response.data as MenuDetails[];
          console.log(response);
        }
      },
      error: error => {
        console.error('Error fetching menu details:', error);
      },
      complete: () => {
        console.log('Menu fetch operation completed');
      }
    })
  }

  ToggleParentMenu(index: number): void {
    this.OpenParentIndex = this.OpenParentIndex === index ? null : index;
  }
}
