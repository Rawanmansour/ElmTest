import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription, fromEvent } from 'rxjs';
import { map, filter, debounceTime } from 'rxjs/operators';


export interface Book {
  bookTitle: string;
  bookDescription: string;
  author: string;
  publishDate: Date;
  coverBase64: string;
}

export interface BookRecord {
  bookId: number;
  bookInfo: Book;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  searchForm!: FormGroup;
  public bookrecords: BookRecord[] = [];
  page = 1;
  pageSize = 15;
  private subscription!: Subscription;
  loading = false;
  showLoader = false;
  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.searchForm = this.fb.group({
      bookTitle: [''],
      author: [''],
      bookDescription: [''],
      publishDate: ['']
    });
  }

  ngOnInit() {
    this.getBooks();
    this.loading = true;
    // Subscribe to the scroll event
    this.subscription = fromEvent(window, 'scroll').pipe(
      map(() => window.scrollY),
      filter(y => y >= document.body.scrollHeight - window.innerHeight),
      debounceTime(200)
    ).subscribe(() => {
      if (!this.loading) {
        this.page++;
        this.showLoader = true; // Show the loader when scrolling
        this.getBooks();
      }
    });
  }

  getBooks() {
    this.loading = true; // Set loading to true when fetching books
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    const params = {
      Page: this.page,
      PageSize: this.pageSize,
      BookTitle: this.searchForm.value.bookTitle,
      Author: this.searchForm.value.author,
      BookDescription: this.searchForm.value.bookDescription,
      PublishDate: this.searchForm.value.publishDate
    };
    this.http.post<BookRecord[]>(`/librarymanagement`,params, httpOptions).subscribe(
      (result) => {
        this.bookrecords.push(...result);
        this.loading = false;
        this.showLoader = false; // Hide the loader after fetching books
      },
      (error) => {
        console.error(error);
        this.loading = false;
        this.showLoader = false; // Hide the loader after fetching books
      }
    );
  }
  onSearch(): void {
    this.bookrecords = [];  // Clear the current book records
    this.page = 1;          // Reset to the first page
    this.getBooks();        // Fetch books based on search criteria
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }


  title = 'librarymangementapp.client';
}
