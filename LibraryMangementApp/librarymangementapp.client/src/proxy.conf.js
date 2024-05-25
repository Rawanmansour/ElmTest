const PROXY_CONFIG = [
  {
    context: [
      "/librarymanagement",
    ],
    target: "https://localhost:7248",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
